using DCS.CoreLib.BaseClass;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents a view model for an Active Directory (AD) user, providing properties and methods to manage and
    /// interact with the underlying <see cref="ADUser"/> model.
    /// </summary>
    /// <remarks>This class serves as a bridge between the UI and the underlying <see cref="ADUser"/> model,
    /// encapsulating the logic for creating, updating, and managing AD user data. It provides properties to access and
    /// modify the user's unique identifier, domain name, and active status, as well as a method to create and register
    /// new AD users.</remarks>
    public class ADUserViewModel : ViewModelBase<Guid, ADUser>
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ADUserViewModel"/> class using the specified Active Directory
        /// user model.
        /// </summary>
        /// <remarks>This constructor sets the <see cref="ViewModelBase{TKey, TModel}.Model"/> property to the provided <paramref
        /// name="adUser"/> instance.</remarks>
        /// <param name="adUser">The Active Directory user model used to initialize the view model. Cannot be <see langword="null"/>.</param>
        public ADUserViewModel(ADUser adUser) : base(adUser)
        {
            this.Model = adUser;
        }

        /// <summary>
        /// Creates a new Active Directory (AD) user and registers it in the system.
        /// </summary>
        /// <remarks>This method generates a new AD user with a unique identifier, associates it with the
        /// current domain,  and marks it as active. The user is then registered using the underlying user service. If
        /// the domain  name is not specified or the registration fails, the method returns <see
        /// langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the AD user is successfully created and registered; otherwise, <see
        /// langword="false"/>.</returns>
        public bool CreateNewADUser()
        {
            if (string.IsNullOrWhiteSpace(this.DomainName))
                return false;

            var adUser = new ADUser
            {
                Guid = Guid.NewGuid(),
                DomainName = Model.DomainName,
                IsActive = true
            };

            if (userService.RegisterADUser(adUser))
            {
                this.Model = adUser;
                return true;
            }

            return false;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="ADUser"/>.
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
        /// Gets or sets the domain name of the <see cref="ADUser"/>.
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
        /// Gets or sets a value indicating whether the current <see cref="ADUser"/> is active.
        /// </summary>
        public bool IsActive
        {
            get => this.Model.IsActive;
            set
            {
                if (this.Model.IsActive != value)
                {
                    this.Model.IsActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }
        #endregion
    }
}
