using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the view model for the <see cref="Organisation"/> model. Implements the <see cref="ViewModelBase{TKey, TModel}"/> class.
    /// </summary>
    public class OrganisationViewModel : ViewModelBase<Guid, Organisation>
    {
        private readonly IOrganisationService organisationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IOrganisationService>();
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IUserAssignementService userAssignementService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementService>();

        private UserAssignementViewModel assignementViewModel;

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="OrganisationViewModel"/>.
        /// </summary>
        public OrganisationViewModel(Organisation organisation) : base(organisation)
        {
            this.Model = organisation;

            Collection = organisationService.GetAll();

            var ua = new UserAssignement();
            assignementViewModel = new UserAssignementViewModel(ua);
        }

        /// <summary>
        /// Creates a new organisation based on the current model and saves it using the organisation service.
        /// </summary>
        /// <remarks>This method initializes a new organisation with the properties specified in the
        /// current model, assigns a unique identifier, and sets default values for its state. The organisation is then
        /// persisted using the organisation service. If the model is null or an error occurs during the creation
        /// process, the method logs the error and returns <see langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the organisation is successfully created and saved; otherwise, <see
        /// langword="false"/>.</returns>
        public bool CreateNewOrganisation()
        {
            if (Model != null)
            {
                try
                {
                    var newOrganisation = new Organisation
                    {
                        Guid = Guid.NewGuid(),
                        Name = Model.Name,
                        Description = Model.Description,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                        LastManipulation = DateTime.Now
                    };

                    if (organisationService.New(newOrganisation))
                        return true;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Error while creating new organisation: {ex.Message}.", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error("Organisation model is null.", "OrganisationViewModel.CreateNewOrganisation");
            return false;
        }

        /// <summary>
        /// Updates the organisation based on the current model. If the organisation exists, its details are updated;
        /// otherwise, a new organisation is created.
        /// </summary>
        /// <remarks>This method first attempts to retrieve an existing organisation using the model's
        /// unique identifier. If found, the organisation's details are updated. If the update fails or the organisation
        /// does not exist, a new organisation is created. Any errors during the update process are logged.</remarks>
        /// <returns><see langword="true"/> if the organisation was successfully updated or created; otherwise, <see
        /// langword="false"/>.</returns>
        public bool UpdateOrganisation()
        {
            if (Model != null && Model.Guid != default)
            {
                var organisation = organisationService.Get(Model.Guid);
                if (organisation != null)
                {
                    try
                    {
                        organisation.Name = Model.Name;
                        organisation.Description = Model.Description;
                        organisation.IsActive = Model.IsActive;
                        organisation.LastManipulation = DateTime.Now;

                        if (organisationService.Update(organisation))
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogManager.Singleton.Error($"Error while updating organisation: {ex.Message}.", $"{ex.Source}");
                        return false;
                    }
                }
            }
            if (CreateNewOrganisation())
                return true;
            else
            {
                Log.LogManager.Singleton.Error("Failed to create new organisation during update process.", "OrganisationViewModel.UpdateOrganisation");
                return false;
            }
        }

        /// <summary>
        /// Retrieves all members of the specified organisation.
        /// </summary>
        /// <remarks>This method queries the user assignments associated with the specified organisation
        /// and retrieves the corresponding user details.</remarks>
        /// <param name="organisation">The organisation for which to retrieve the members. Cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="User"/> objects representing the members of the
        /// specified organisation.  Returns an empty collection if the organisation has no members or if the
        /// organisation is <see langword="null"/>.</returns>
        public ObservableCollection<User> GetAllOrganisationMember(Organisation organisation)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            if (organisation != null)
            {
                ObservableCollection<UserAssignement> userAssignments = userAssignementService.GetAll();

                if (userAssignments != null && userAssignments.Count > 0)
                {
                    foreach (var userAssignment in userAssignments)
                    {
                        if (userAssignment.OrganisationGuid == organisation.Guid)
                        {
                            var user = userService.Get(userAssignment.UserGuid);
                            if (user != null)
                            {
                                users.Add(user);
                            }
                        }
                    }
                }

                return users;
            }

            return users;
        }

        /// <summary>
        /// Adds the specified user to the current organization.
        /// </summary>
        /// <remarks>This method attempts to associate the specified user with the organization
        /// represented by the current model. Ensure that both the user and the organization are valid before calling
        /// this method.</remarks>
        /// <param name="user">The user to be added to the organization. The user must not be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the organization; otherwise, <see
        /// langword="false"/>.</returns>
        public bool AddUserToOrganisation(User user)
        {
            if (user != null && Model != null)
            {
                if (assignementViewModel.AddUserToOrganisation(user.Guid, Model.Guid))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified user from the current organization.
        /// </summary>
        /// <remarks>This method attempts to remove the specified user from the organization associated
        /// with the current model. If the user is not assigned to the organization or if the operation fails, the
        /// method returns <see langword="false"/>.</remarks>
        /// <param name="user">The user to be removed from the organization. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the organization; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromOrganisation(User user)
        {
            if (user != null && Model != null)
            {
                if (assignementViewModel.RemoveUserFromOrganisation(user.Guid, Model.Guid))
                {
                    Collection.Remove(Model);
                    return true;
                }

                Log.LogManager.Singleton.Error($"Failed to remove user {user.UserName} from organisation {Model.Name}.", "OrganisationViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or organisation model is null during removal from organisation.", "OrganisationViewModel");
            return false;
        }

        #region Properties
        /// <summary>
        /// Organisation unique identifier.
        /// </summary>
        public Guid Guid
        {
            get { return Model.Guid; }
            set
            {
                if (Model.Guid != value)
                {
                    Model.Guid = value;
                    OnPropertyChanged(nameof(Guid));
                }
            }
        }

        /// <summary>
        /// Organisation ident.
        /// </summary>
        public int Ident
        {
            get { return Model.Ident; }
            set
            {
                if (Model.Ident != value)
                {
                    Model.Ident = value;
                    OnPropertyChanged(nameof(Ident));
                }
            }
        }

        /// <summary>
        /// Organisation name.
        /// </summary>
        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Organisation description.
        /// </summary>
        public string Description
        {
            get { return Model.Description; }
            set
            {
                if (Model.Description != value)
                {
                    Model.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        /// <summary>
        /// Organisation is active flag.
        /// </summary>
        public bool IsActive
        {
            get { return Model.IsActive; }
            set
            {
                if (Model.IsActive != value)
                {
                    Model.IsActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        /// <summary>
        /// Organisation creation date.
        /// </summary>
        public DateTime? CreationDate
        {
            get { return Model.CreationDate; }
            set
            {
                if (Model.CreationDate != value)
                {
                    Model.CreationDate = value;
                    OnPropertyChanged(nameof(CreationDate));
                }
            }
        }

        /// <summary>
        /// Organisation properties last time changed date.
        /// </summary>
        public DateTime? LastManipulation
        {
            get { return Model.LastManipulation; }
            set
            {
                if (Model.LastManipulation != value)
                {
                    Model.LastManipulation = value;
                    OnPropertyChanged(nameof(LastManipulation));
                }
            }
        }
        #endregion
    }
}
