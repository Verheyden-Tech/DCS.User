using DCS.Authorisation;
using DCS.CoreLib.BaseClass;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents a view model for an Active Directory (AD) user, providing properties and methods to manage and
    /// interact with the underlying <see cref="UserDomain"/> model.
    /// </summary>
    /// <remarks>This class serves as a bridge between the UI and the underlying <see cref="UserDomain"/> model,
    /// encapsulating the logic for creating, updating, and managing AD user data. It provides properties to access and
    /// modify the user's unique identifier, domain name, and active status, as well as a method to create and register
    /// new AD users.</remarks>
    public class UserDomainViewModel : ViewModelBase<Guid, UserDomain>
    {
        private readonly IUserDomainService domainService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserDomainService>();
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IDomainSubscriptionService subscriptionService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IDomainSubscriptionService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainViewModel"/> class using the specified Active Directory
        /// user model.
        /// </summary>
        /// <remarks>This constructor sets the <see cref="ViewModelBase{TKey, TModel}.Model"/> property to the provided <paramref
        /// name="userDomain"/> instance.</remarks>
        /// <param name="userDomain">The Active Directory user model used to initialize the view model. Cannot be <see langword="null"/>.</param>
        public UserDomainViewModel(UserDomain userDomain) : base(userDomain)
        {
            Model = userDomain;
            Collection = domainService.GetAll().Result;
        }

        #region Create, Update, Delete Domain
        /// <summary>
        /// Creates a new domain and its associated subscription if the domain does not already exist.
        /// </summary>
        /// <remarks>This method checks if the domain specified in the <c>Model.DomainName</c> property is
        /// valid and does not already exist.  If the domain is valid and does not exist, a new domain and subscription
        /// are created and persisted.  If the operation fails at any step, an error is logged, and the method returns
        /// <see langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the domain and subscription are successfully created; otherwise, <see
        /// langword="false"/>.</returns>
        public bool CreateNewDomain()
        {
            if (string.IsNullOrWhiteSpace(Model.DomainName))
                return false;

            try
            {
                var userDomain = new UserDomain
                {
                    Guid = Guid.NewGuid(),
                    DomainName = Model.DomainName,
                    SubscriptionActive = true,
                    StartSubscription = DateTime.UtcNow
                };

                var subscription = new DomainSubscription
                {
                    Guid = Guid.NewGuid(),
                    DomainGuid = userDomain.Guid,
                    DomainName = userDomain.DomainName,
                    SubscriptionGuid = Guid.NewGuid(),
                    SubscriptionCreated = DateTime.UtcNow,
                    SubscriptionUpdated = DateTime.UtcNow,
                    SubscriptionStart = DateTime.UtcNow
                };

                try
                {
                    if (subscriptionService.New(subscription).Result)
                    {
                        userDomain.LicenceKey = subscription.SubscriptionGuid;

                        try
                        {
                            if (domainService.New(userDomain).Result)
                                return true;
                            return false;
                        }
                        catch (Exception ex)
                        {
                            Log.LogManager.Singleton.Error($"Failed to create domain: {Model.DomainName}. Exception: {ex.Message}", $"{ex.Source}");
                            return false;
                        }
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Failed to create subscription for domain: {Model.DomainName}. Exception: {ex.Message}", $"{ex.Source}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.LogManager.Singleton.Error($"Failed to create new domain: {Model.DomainName}. Exception: {ex.Message}", $"{ex.Source}");
                return false;
            }
        }

        /// <summary>
        /// Updates the domain information associated with the current model.
        /// </summary>
        /// <remarks>This method updates the domain details in the system if the domain exists and the
        /// model contains valid data. If the domain does not exist or the model's domain name is null, empty, or
        /// whitespace, the update operation will fail. Logs are written to capture errors or when the domain is not
        /// found.</remarks>
        /// <returns><see langword="true"/> if the domain was successfully updated; otherwise, <see langword="false"/>.</returns>
        public bool UpdateDomain()
        {
            if (string.IsNullOrWhiteSpace(Model.DomainName))
                return false;

            if (Model.Guid != default)
            {
                if (domainService.Get(Model.Guid) != null)
                {
                    var subscription = subscriptionService.GetAll().Result.Where(s => s.SubscriptionGuid == Model.LicenceKey).FirstOrDefault();
                    if (subscription != null)
                    {
                        try
                        {
                            subscription.SubscriptionUpdated = DateTime.UtcNow;
                            subscription.SubscriptionEnd = Model.EndSubscription;
                            subscription.DomainName = Model.DomainName;

                            if (subscriptionService.Update(subscription).Result)
                            {
                                Model.SubscriptionActive = subscription.SubscriptionEnd > DateTime.UtcNow;

                                if (domainService.Update(Model).Result)
                                    return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.LogManager.Singleton.Error($"Failed to update domain: {Model.DomainName}. Exception: {ex.Message}", $"{ex.Source}");
                            return false;
                        }
                    }

                    Log.LogManager.Singleton.Error($"Subscription not found for domain: {Model.DomainName}", "UserDomainViewModel.UpdateDomain");
                    return false;
                }

                Log.LogManager.Singleton.Error($"Domain not found: {Model.DomainName}", "UserDomainViewModel.UpdateDomain");
                return false;
            }
            else
            {
                if (CreateNewDomain())
                    return true;

                Log.LogManager.Singleton.Error($"Failed to create new domain: {Model.DomainName}", "UserDomainViewModel.UpdateDomain");
                return false;
            }
        }

        /// <summary>
        /// Deletes the domain associated with the current model, if it exists.
        /// </summary>
        /// <remarks>This method attempts to delete the domain identified by the <c>Guid</c> property of
        /// the current model. If the domain does not exist or the deletion fails, the method logs an error and returns
        /// <see langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the domain was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public bool DeleteDomain()
        {
            if (string.IsNullOrWhiteSpace(Model.DomainName))
                return false;

            if (domainService.Get(Model.Guid) != null)
            {
                try
                {
                    if (domainService.Delete(Model.Guid).Result)
                        return true;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Failed to delete domain: {Model.DomainName}. Exception: {ex.Message}", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error($"Domain not found: {Model.DomainName}", "UserDomainViewModel.DeleteDomain");
            return false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="UserDomain"/>.
        /// </summary>
        public Guid Guid
        {
            get => this.Model.Guid;
            set
            {
                if (this.Model.Guid != value)
                {
                    this.Model.Guid = value;
                    OnPropertyChanged(nameof(Guid));
                }
            }
        }

        /// <summary>
        /// Gets or sets the domain name of the <see cref="UserDomain"/>.
        /// </summary>
        public string DomainName
        {
            get => this.Model.DomainName;
            set
            {
                if (this.Model.DomainName != value)
                {
                    this.Model.DomainName = value;
                    OnPropertyChanged(nameof(DomainName));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current <see cref="UserDomain"/> is active.
        /// </summary>
        public bool SubscriptionActive
        {
            get => this.Model.SubscriptionActive;
            set
            {
                if (this.Model.SubscriptionActive != value)
                {
                    this.Model.SubscriptionActive = value;
                    OnPropertyChanged(nameof(SubscriptionActive));
                }
            }
        }

        /// <summary>
        /// Gets or sets the license key associated with the application.
        /// </summary>
        public Guid LicenceKey
        {
            get => this.Model.LicenceKey;
            set
            {
                if (this.Model.LicenceKey != value)
                {
                    this.Model.LicenceKey = value;
                    OnPropertyChanged(nameof(LicenceKey));
                }
            }
        }

        /// <summary>
        /// Gets or sets the start date of the subscription.
        /// </summary>
        public DateTime StartSubscription
        {
            get => this.Model.StartSubscription;
            set
            {
                if (this.Model.StartSubscription != value)
                {
                    this.Model.StartSubscription = value;
                    OnPropertyChanged(nameof(StartSubscription));
                }
            }
        }

        /// <summary>
        /// Gets or sets the date and time when the subscription ends.
        /// </summary>
        public DateTime? EndSubscription
        {
            get => this.Model.EndSubscription;
            set
            {
                if (this.Model.EndSubscription != value)
                {
                    this.Model.EndSubscription = value;
                    OnPropertyChanged(nameof(EndSubscription));
                }
            }
        }
        #endregion
    }
}
