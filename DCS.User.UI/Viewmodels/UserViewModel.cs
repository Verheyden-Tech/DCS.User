using DCS.CoreLib.BaseClass;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Implements the <see cref="ViewModelBase{TKey, TModel}"/> class for the <see cref="User"/> model.
    /// </summary>
    public class UserViewModel : ViewModelBase<Guid, User>
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();
        private readonly IOrganisationService organisationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IOrganisationService>();
        private readonly IRoleService roleService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IRoleService>();
        private readonly IUserAssignementService assignementService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementService>();

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
        #endregion

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="UserViewModel"/>.
        /// </summary>
        public UserViewModel(User user) : base(user)
        {
            this.Model = user;

            groups = groupService.GetAll();
            organisations = organisationService.GetAll();
            roles = roleService.GetAll();
        }

        /// <summary>
        /// Saves the current instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <returns>True if the save was successful; otherwise, false.</returns>
        public bool Save()
        {
            if (Model != null)
            {
                Model.LastManipulation = DateTime.Now;

                if (userService.Update(Model))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

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
                    if (assignementService.AddUserToGroup(Model.Guid, group.Guid))
                    {
                        UserGroups.Add(group);
                        return true;
                    }

                    return false;
                }

                return false;
            }

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
                if(UserGroups.Contains(group))
                {
                    var assignement = assignementService.GetByUserAndGroup(Model.Guid, group.Guid);

                    if(assignement != null)
                    {
                        if (assignementService.RemoveUserFromGroup(assignement))
                        {
                            UserGroups.Remove(group);

                            return true;
                        }

                        return false;
                    }

                    return false;
                }

                return false;
            }

            return false;
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
                    if (assignementService.AddUserToOrganisation(Model.Guid, organisation.Guid))
                    {
                        UserOrganisations.Add(organisation);

                        return true;
                    }

                    return false;
                }

                return false;
            }

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
                    var assignement = assignementService.GetByUserAndOrganisation(Model.Guid, organisation.Guid);
                    if (assignement != null)
                    {
                        if (assignementService.RemoveUserFromOrganisation(assignement))
                        {
                            UserOrganisations.Remove(organisation);

                            return true;
                        }

                        return false;
                    }

                    return false;
                }

                return false;
            }

            return false;
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
                    if (assignementService.AddUserToRole(Model.Guid, role.Guid))
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
                    var assignement = assignementService.GetByUserAndRole(Model.Guid, role.Guid);
                    if (assignement != null)
                    {
                        if (assignementService.RemoveUserFromRole(assignement))
                        {
                            UserRoles.Remove(role);

                            return true;
                        }

                        return false;
                    }

                    return false;
                }

                return false;
            }

            return false;
        }
        #endregion

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
                Model.ProfilePicturePath = openFileDialog.FileName;
                OnPropertyChanged(nameof(Model.ProfilePicturePath));
                return true;
            }

            return false;
        }

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
        /// Gets or sets the user identifier.
        /// </summary>
        public int Ident
        {
            get => Model.Ident;
            set => Model.Ident = value;
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
        /// Gets or sets the user is logged in in current session flag.
        /// </summary>
        public bool IsLoggedIn
        {
            get => Model.IsLoggedIn;
            set => Model.IsLoggedIn = value;
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
