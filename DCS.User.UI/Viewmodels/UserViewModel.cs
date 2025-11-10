using DCS.CoreLib.BaseClass;
using DCS.User.Service;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace DCS.User.UI
{
    /// <summary>
    /// Implements the <see cref="ViewModelBase{TKey, TModel}"/> class for the <see cref="User"/> model.
    /// </summary>
    public class UserViewModel : ViewModelBase<Guid, User>, INotifyPropertyChanged
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();
        private readonly IOrganisationService organisationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IOrganisationService>();
        private readonly IRoleService roleService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IRoleService>();
        private readonly IUserAssignementService assignementService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementService>();
        private readonly IUserDomainService domainService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserDomainService>();

        private UserAssignementViewModel assignementViewModel;

        #region List Initializations
        /// <summary>
        /// Contains all avialable user groups from the table.
        /// </summary>
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();
        /// <summary>
        /// Contains all avialable user organisations from the table.
        /// </summary>
        private ObservableCollection<Organisation> organisations = new ObservableCollection<Organisation>();
        /// <summary>
        /// Contains all avialable user roles from the table.
        /// </summary>
        private ObservableCollection<Role> roles = new ObservableCollection<Role>();
        /// <summary>
        /// Contains all added user groups.
        /// </summary>
        private ObservableCollection<Group> userGroups = new ObservableCollection<Group>();
        /// <summary>
        /// Contains all added user organisations.
        /// </summary>
        private ObservableCollection<Organisation> userOrganisations = new ObservableCollection<Organisation>();
        /// <summary>
        /// Contains all added user roles.
        /// </summary>
        private ObservableCollection<Role> userRoles = new ObservableCollection<Role>();
        /// <summary>
        /// Represents a collection of user domains that can be observed for changes.
        /// </summary>
        /// <remarks>This collection is used to store and manage instances of <see cref="UserDomain"/>. 
        /// Changes to the collection, such as additions or removals, will raise notifications  to any observers, making
        /// it suitable for data binding scenarios.</remarks>
        private ObservableCollection<UserDomain> domains = new ObservableCollection<UserDomain>();
        /// <summary>
        /// Represents a collection of available languages as <see cref="CultureInfo"/> objects.
        /// </summary>
        /// <remarks>This collection is intended to store the languages supported by the application.  It
        /// can be used to populate language selection options or to manage localization settings.</remarks>
        private ObservableCollection<CultureInfo> avialableLanguages = new ObservableCollection<CultureInfo>();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class using the specified user.
        /// </summary>
        /// <remarks>This constructor populates the view model with data related to the specified user,
        /// including their groups, organizations, and roles. It also retrieves all available groups, organizations, and
        /// roles for reference.</remarks>
        /// <param name="user">The user model used to initialize the view model. Cannot be <see langword="null"/>.</param>
        public UserViewModel(User user) : base(user)
        {
            this.Model = user;

            //AvialableLanguages = CurrentLanguageService.Instance.GetAvailableLanguages();

            Collection = userService.GetAll();

            var ua = new UserAssignement();
            assignementViewModel = new UserAssignementViewModel(ua);

            if (user.Guid != default)
            {
                UserGroups = GetUserGroups(user);
                UserOrganisations = GetUserOrganisations(user);
                UserRoles = GetUserRoles(user);
            }

            Domains = domainService.GetAll();
        }

        /// <summary>
        /// Refreshes the list of domains by clearing the current collection and retrieving the latest data.
        /// </summary>
        /// <remarks>This method attempts to update the domain list by invoking the domain service.  If an
        /// error occurs during the operation, the method logs the error and returns <see langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the domain list was successfully refreshed; otherwise, <see langword="false"/>.</returns>
        public bool RefreshDomains()
        {
            try
            {
                Domains.Clear();
                Domains = domainService.GetAll();
                return true;
            }
            catch (Exception ex)
            {
                Log.LogManager.Singleton.Error($"Error while refreshing domains: {ex.Message}.", $"{ex.Source}");
                return false;
            }
        }

        /// <summary>
        /// Retrieves all user domains.
        /// </summary>
        /// <remarks>This method fetches all user domains from the underlying data source.  If an error
        /// occurs during retrieval, an empty collection is returned, and the error is logged.</remarks>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="UserDomain"/> objects representing all user domains. 
        /// Returns an empty collection if an error occurs.</returns>
        public ObservableCollection<UserDomain> GetAllDomains()
        {
            try
            {
                return domainService.GetAll();
            }
            catch (Exception ex)
            {
                Log.LogManager.Singleton.Error($"Error while retrieving all domains: {ex.Message}.", $"{ex.Source}");
                return new ObservableCollection<UserDomain>();
            }
        }

        #region Register, Update, Delete, Login, Set Profile Picture for User
        /// <summary>
        /// Registers a new user with the specified credentials and settings.
        /// </summary>
        /// <remarks>This method attempts to create and register a user based on the current instance's
        /// properties. The operation will fail if the <see cref="Domain"/> property is null or empty, or if the user
        /// creation process encounters an error.</remarks>
        /// <returns><see langword="true"/> if the user was successfully registered; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the user creation process returns a null user object.</exception>
        public bool RegistrateUser()
        {
            if (Model != null)
            {
                try
                {
                    var newUser = new User
                    {
                        Guid = Guid.NewGuid(),
                        UserName = Model.UserName,
                        PassWord = CryptographyHelper.HashSHA256(Model.PassWord),
                        Domain = CurrentDomainService.Instance.CurrentDomain.DomainName,
                        IsActive = true,
                        IsAdmin = Model.IsAdmin,
                        IsADUser = false,
                        KeepLoggedIn = Model.KeepLoggedIn,
                        CreationDate = DateTime.Today,
                        LastManipulation = DateTime.Today,
                        ProfilePicturePath = Model.ProfilePicturePath
                    };

                    if (userService.New(newUser))
                    {
                        Model = newUser;
                        CurrentUserService.Instance.SetUser(newUser);
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Error while creating user: {ex.Message}.", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error("User model is null during registration.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Updates the user information in the system. If the user does not exist, attempts to create a new user.
        /// </summary>
        /// <remarks>This method first checks if the user model is valid and whether the user already
        /// exists in the system. If the user exists, their information is updated. If the user does not exist, a new
        /// user is created. Any errors encountered during the update or creation process are logged.</remarks>
        /// <returns><see langword="true"/> if the user was successfully updated or created; otherwise, <see langword="false"/>.</returns>
        public bool UpdateUser()
        {
            if (Model != null)
            {
                if (userService.Get(Model.Guid) != null)
                {
                    try
                    {
                        Model.LastManipulation = DateTime.Now;

                        if (userService.Update(Model))
                        {
                            Log.LogManager.Singleton.Warning($"User {Model.UserName} updated successfully.", "UserViewModel");
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogManager.Singleton.Error($"Error while updating user: {ex.Message}.", $"{ex.Source}");
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        if (RegistrateUser())
                        {
                            Log.LogManager.Singleton.Warning($"User {Model.UserName} created successfully.", "UserViewModel");
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogManager.Singleton.Error($"Error while creating user: {ex.Message}.", $"{ex.Source}");
                        return false;
                    }
                }

                Log.LogManager.Singleton.Error("User model is null during update.", "UserViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model is null during update.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Deletes the user associated with the current model.
        /// </summary>
        /// <remarks>This method attempts to delete the user identified by its <see cref="Guid"/>
        /// property. If the deletion is successful, a warning log entry is created. If the deletion fails or an
        /// exception occurs, an error log entry is created, and the method returns <see langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the user was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public bool DeleteUser()
        {
            if (Model != null)
            {
                try
                {
                    if (userService.Delete(Model.Guid))
                    {
                        Log.LogManager.Singleton.Warning($"User {Model.UserName} deleted successfully.", "UserViewModel");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Error while deleting user: {ex.Message}.", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error("User model is null during deletion.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Attempts to log in a user based on the provided user model and credentials.
        /// </summary>
        /// <remarks>This method verifies the user's credentials by comparing the provided username,
        /// password, and domain against the stored user data. If the credentials are valid, the user is set as the
        /// current user in the system.</remarks>
        /// <returns><see langword="true"/> if the login is successful; otherwise, <see langword="false"/>.</returns>
        public bool LoginUser(string rawPassword)
        {
            if (Model != null)
            {
                var user = Collection.FirstOrDefault(u => u.UserName == Model.UserName);
                if (user != null)
                {
                    var hasehedPassword = CryptographyHelper.HashSHA256(rawPassword);
                    if (user.PassWord == hasehedPassword)
                    {
                        CurrentUserService.Instance.SetUser(user);
                        Log.LogManager.Singleton.Warning($"User {Model.UserName} logged in successfully.", "UserViewModel");
                        return true;
                    }

                    Log.LogManager.Singleton.Warning($"Failed login attempt for user account {Model.UserName}", "UserViewModel");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error("User model is null during login.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Opens a file dialog to allow the user to select an image file and sets the selected file  as the user's
        /// profile picture.
        /// </summary>
        /// <remarks>The file dialog filters for common image file formats, including .jpg, .jpeg, .png,
        /// .gif, and .bmp. If a file is selected, the profile picture path is updated and a property change
        /// notification is raised.</remarks>
        /// <returns><see langword="true"/> if the user successfully selects an image file; otherwise,  <see langword="false"/>.</returns>
        public bool SetUserProfilePicture()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Profilbild auswählen"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Model.ProfilePicturePath = openFileDialog.FileName;
                    OnPropertyChanged(nameof(Model.ProfilePicturePath));
                    return true;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Error while setting profile picture for {Model.UserName}: {ex.Message}.", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Warning($"No profile picture selected for {Model.UserName}.", "UserViewModel");
            return false;
        }
        #endregion

        #region Add/Remove User from Group/Organisation/Role Methods
        /// <summary>
        /// Adds the current user to the specified group.
        /// </summary>
        /// <remarks>This method checks if the user is already a member of the specified group before
        /// attempting to add them. If the user is already a member or if the operation fails, the method returns <see
        /// langword="false"/>.</remarks>
        /// <param name="group">The group to which the user will be added. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the group; otherwise, <see langword="false"/>.</returns>
        public bool AddUserToGroup(Group group)
        {
            if (Model != null && group != null)
            {
                if (!UserGroups.Contains(group))
                {
                    if (assignementViewModel.AddUserToGroup(Model.Guid, group.Guid))
                    {
                        UserGroups.Add(group);
                        return true;
                    }

                    Log.LogManager.Singleton.Error($"Failed to add user {Model.UserName} to group {group.Name}.", "UserViewModel");
                    return false;
                }

                Log.LogManager.Singleton.Error($"User {Model.UserName} is already a member of group {group.Name}.", "UserViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or group is null during addition to group.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Removes the current user from the specified group.
        /// </summary>
        /// <remarks>This method checks if the user is a member of the specified group and attempts to
        /// remove the user from it. The operation will fail if the group is not found, the user is not a member of the
        /// group, or if the removal process encounters an issue.</remarks>
        /// <param name="group">The group from which the user should be removed. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the group; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromGroup(Group group)
        {
            if (Model != null && group != null)
            {
                if (UserGroups.Contains(group))
                {
                    if (assignementViewModel.RemoveUserFromGroup(Model.Guid, group.Guid))
                    {
                        UserGroups.Remove(group);
                        return true;
                    }

                    Log.LogManager.Singleton.Error($"Failed to remove user {Model.UserName} from group {group.Name}.", "UserViewModel");
                    return false;
                }

                Log.LogManager.Singleton.Error($"User {Model.UserName} is not a member of group {group.Name}.", "UserViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or group is null during removal from group.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Retrieves the collection of groups associated with the specified user.
        /// </summary>
        /// <remarks>This method filters the user's group assignments based on valid group identifiers and
        /// retrieves the corresponding group objects from the available groups. If a group associated with the user
        /// cannot be found, it is skipped.</remarks>
        /// <param name="user">The user whose groups are to be retrieved. Cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="Group"/> objects representing the groups the
        /// specified user is assigned to. Returns an empty collection if the user is not assigned to any groups or if
        /// <paramref name="user"/> is <see langword="null"/>.</returns>
        public ObservableCollection<Group> GetUserGroups(User user)
        {
            var userGroups = new ObservableCollection<Group>();
            var assignments = new ObservableCollection<UserAssignement>(assignementService.GetAll());

            if (user != null)
            {
                foreach (var assignment in assignments.Where(ua => ua.UserGuid == user.Guid && ua.GroupGuid != default))
                {
                    var group = AllGroups.FirstOrDefault(g => g.Guid == assignment.GroupGuid);
                    if (group != null)
                    {
                        userGroups.Add(group);
                        continue;
                    }
                }

                return userGroups;
            }

            return userGroups;
        }

        /// <summary>
        /// Adds the current user to the specified organisation.
        /// </summary>
        /// <remarks>This method ensures that the user is not already a member of the specified
        /// organisation before attempting to add them. The operation will fail if the organisation is <see
        /// langword="null"/> or if the user is already associated with the organisation.</remarks>
        /// <param name="organisation">The organisation to which the user will be added. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the organisation; otherwise, <see
        /// langword="false"/>.</returns>
        public bool AddUserToOrganisation(Organisation organisation)
        {
            if (Model != null && organisation != null)
            {
                if (!UserOrganisations.Contains(organisation))
                {
                    if (assignementViewModel.AddUserToOrganisation(Model.Guid, organisation.Guid))
                    {
                        UserOrganisations.Add(organisation);
                        return true;
                    }

                    Log.LogManager.Singleton.Error($"Failed to add user {Model.UserName} to organisation {organisation.Name}.", "UserViewModel");
                    return false;
                }

                Log.LogManager.Singleton.Error($"User {Model.UserName} is already a member of organisation {organisation.Name}.", "UserViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or organisation is null during addition to organisation.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Removes the current user from the specified organisation.
        /// </summary>
        /// <remarks>This method checks if the user is associated with the specified organisation and, if
        /// so,  attempts to remove the user. The operation will fail if the user is not associated with the 
        /// organisation or if the removal process encounters an issue.</remarks>
        /// <param name="organisation">The organisation from which the user should be removed. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the organisation;  otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromOrganisation(Organisation organisation)
        {
            if (Model != null && organisation != null)
            {
                if (UserOrganisations.Contains(organisation))
                {
                    if (assignementViewModel.RemoveUserFromOrganisation(Model.Guid, organisation.Guid))
                    {
                        UserOrganisations.Remove(organisation);
                        return true;
                    }

                    Log.LogManager.Singleton.Error($"Failed to remove user {Model.UserName} from organisation {organisation.Name}.", "UserViewModel");
                    return false;
                }

                Log.LogManager.Singleton.Error($"User {Model.UserName} is not a member of organisation {organisation.Name}.", "UserViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or organisation is null during removal from organisation.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Retrieves a collection of organisations associated with the specified user.
        /// </summary>
        /// <remarks>This method filters organisations based on the user's assignments, ensuring that only
        /// valid organisations  with non-empty GUIDs are included in the result.</remarks>
        /// <param name="user">The user whose associated organisations are to be retrieved. Cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="Organisation"/> objects representing the
        /// organisations  linked to the specified user. Returns an empty collection if the user has no associated
        /// organisations or if  <paramref name="user"/> is <see langword="null"/>.</returns>
        public ObservableCollection<Organisation> GetUserOrganisations(User user)
        {
            var userOrganisations = new ObservableCollection<Organisation>();
            var assignments = new ObservableCollection<UserAssignement>(assignementService.GetAll());

            if (user != null)
            {
                foreach (var assignment in assignments.Where(ua => ua.UserGuid == user.Guid && ua.OrganisationGuid != default))
                {
                    var organisation = AllOrganisations.FirstOrDefault(o => o.Guid == assignment.OrganisationGuid);

                    if (organisation != null)
                    {
                        userOrganisations.Add(organisation);
                    }
                }

                return userOrganisations;
            }

            return userOrganisations;
        }

        /// <summary>
        /// Adds the specified role to the user if it is not already assigned.
        /// </summary>
        /// <remarks>This method checks if the role is not already assigned to the user before attempting
        /// to add it.  If the role is successfully added, it is also added to the local collection of user
        /// roles.</remarks>
        /// <param name="role">The role to assign to the user. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the role was successfully added to the user;  otherwise, <see langword="false"/>.</returns>
        public bool AddUserToRole(Role role)
        {
            if (Model != null && role != null)
            {
                if (!UserRoles.Contains(role))
                {
                    if (assignementViewModel.AddUserToRole(Model.Guid, role.Guid))
                    {
                        UserRoles.Add(role);

                        return true;
                    }

                    return false;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified role from the current user.
        /// </summary>
        /// <remarks>This method attempts to remove the specified role from the current user. The
        /// operation will fail if: <list type="bullet"> <item><description>The <paramref name="role"/> is not assigned
        /// to the user.</description></item> <item><description>The role or user data cannot be retrieved or updated
        /// due to an internal issue.</description></item> </list> Ensure that the <paramref name="role"/> parameter is
        /// not <see langword="null"/> before calling this method.</remarks>
        /// <param name="role">The role to be removed from the user. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the role was successfully removed from the user; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromRole(Role role)
        {
            if (Model != null && role != null)
            {
                if (UserRoles.Contains(role))
                {
                    if (assignementViewModel.RemoveUserFromRole(Model.Guid, role.Guid))
                    {
                        UserRoles.Remove(role);
                        return true;
                    }

                    Log.LogManager.Singleton.Error($"Failed to remove user {Model.UserName} from role {role.Name}.", "UserViewModel");
                    return false;
                }

                Log.LogManager.Singleton.Error($"User {Model.UserName} is not a member of role {role.Name}.", "UserViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or role is null during removal from role.", "UserViewModel");
            return false;
        }

        /// <summary>
        /// Retrieves the roles assigned to the specified user.
        /// </summary>
        /// <remarks>This method filters roles based on the user's assignments and excludes any roles with
        /// an empty GUID. The returned collection is dynamically observable, meaning changes to the collection will
        /// notify any bound UI or listeners.</remarks>
        /// <param name="user">The user for whom to retrieve roles. Cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="Role"/> objects representing the roles assigned to
        /// the user. If the user has no roles or if <paramref name="user"/> is <see langword="null"/>, an empty
        /// collection is returned.</returns>
        public ObservableCollection<Role> GetUserRoles(User user)
        {
            var userRoles = new ObservableCollection<Role>();
            var assignments = new ObservableCollection<UserAssignement>(assignementService.GetAll());

            if (user != null)
            {
                foreach (var assignment in assignments.Where(ua => ua.UserGuid == user.Guid && ua.RoleGuid != default))
                {
                    var role = AllRoles.FirstOrDefault(r => r.Guid == assignment.RoleGuid);

                    if (role != null)
                    {
                        userRoles.Add(role);
                    }
                }

                return userRoles;
            }

            return userRoles;
        }
        #endregion

        #region Lists
        /// <summary>
        /// Contains all avialable user groups from the table.
        /// </summary>
        public ObservableCollection<Group> AllGroups
        {
            get => groups;
            set
            {
                if (groups != value)
                {
                    groups = value;
                    OnPropertyChanged(nameof(AllGroups));
                }
            }
        }

        /// <summary>
        /// Contains all added user groups.
        /// </summary>
        public ObservableCollection<Group> UserGroups
        {
            get => userGroups;
            set
            {
                if (userGroups != value)
                {
                    userGroups = value;
                    OnPropertyChanged(nameof(UserGroups));
                }
            }
        }

        /// <summary>
        /// Contains all avialable user organisations from the table.
        /// </summary>
        public ObservableCollection<Organisation> AllOrganisations
        {
            get => organisations;
            set
            {
                if (organisations != value)
                {
                    organisations = value;
                    OnPropertyChanged(nameof(AllOrganisations));
                }
            }
        }

        /// <summary>
        /// Contains all added user organisations.
        /// </summary>
        public ObservableCollection<Organisation> UserOrganisations
        {
            get => userOrganisations;
            set
            {
                if (userOrganisations != value)
                {
                    userOrganisations = value;
                    OnPropertyChanged(nameof(UserOrganisations));
                }
            }
        }

        /// <summary>
        /// Contains all avialable user roles from the table.
        /// </summary>
        public ObservableCollection<Role> AllRoles
        {
            get => roles;
            set
            {
                if (roles != value)
                {
                    roles = value;
                    OnPropertyChanged(nameof(AllRoles));
                }
            }
        }

        /// <summary>
        /// Contains all added user roles.
        /// </summary>
        public ObservableCollection<Role> UserRoles
        {
            get => userRoles;
            set
            {
                if (userRoles != value)
                {
                    userRoles = value;
                    OnPropertyChanged(nameof(UserRoles));
                }
            }
        }

        /// <summary>
        /// Gets or sets the collection of user domains associated with the current context.
        /// </summary>
        public ObservableCollection<UserDomain> Domains
        {
            get => domains;
            set
            {
                if (domains != value)
                {
                    domains = value;
                    OnPropertyChanged(nameof(Domains));
                }
            }
        }

        /// <summary>
        /// Gets or sets the collection of available languages supported by the application.
        /// </summary>
        public ObservableCollection<CultureInfo> AvialableLanguages
        {
            get => avialableLanguages;
            set
            {
                if (avialableLanguages != value)
                {
                    avialableLanguages = value;
                    OnPropertyChanged(nameof(AvialableLanguages));
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Guid
        {
            get => Model.Guid;
            set => Model.Guid = value;
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName
        {
            get => Model.UserName;
            set => Model.UserName = value;
        }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string PassWord
        {
            get => Model.PassWord;
            set => Model.PassWord = value;
        }

        /// <summary>
        /// Gets or sets the user domain name.
        /// </summary>
        public string Domain
        {
            get => Model.Domain;
            set => Model.Domain = value;
        }

        /// <summary>
        /// Gets or sets the user is admin flag.
        /// </summary>
        public bool IsActive
        {
            get => Model.IsActive;
            set => Model.IsActive = value;
        }

        /// <summary>
        /// Gets or sets the user keep logged in flag.
        /// </summary>
        public bool KeepLoggedIn
        {
            get => Model.KeepLoggedIn;
            set => Model.KeepLoggedIn = value;
        }

        /// <summary>
        /// Gets or sets the user has admin rights flag.
        /// </summary>
        public bool IsAdmin
        {
            get => Model.IsAdmin;
            set => Model.IsAdmin = value;
        }

        /// <summary>
        /// Gets or sets the user is local user flag.
        /// </summary>
        public bool IsADUser
        {
            get => Model.IsADUser;
            set => Model.IsADUser = value;
        }

        /// <summary>
        /// Gets or sets the user creation date.
        /// </summary>
        public DateTime? CreationDate
        {
            get => Model.CreationDate;
            set => Model.CreationDate = value;
        }

        /// <summary>
        /// Gets or sets the user substitution ending date.
        /// </summary>
        public DateTime? SubstitutionEnd
        {
            get => Model.SubstitutionEnd;
            set => Model.SubstitutionEnd = value;
        }

        /// <summary>
        /// Gets or sets the user last manipulation date.
        /// </summary>
        public DateTime? LastManipulation
        {
            get => Model.LastManipulation;
            set => Model.LastManipulation = value;
        }

        /// <summary>
        /// Gets or sets the file path to the user's profile picture.
        /// </summary>
        public string? ProfilePicturePath
        {
            get => Model.ProfilePicturePath;
            set => Model.ProfilePicturePath = value;
        }
        #endregion
    }
}
