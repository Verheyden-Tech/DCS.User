using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the ViewModel for the <see cref="Role"/> model. Implements the <see cref="ViewModelBase{TKey, TModel}"/> class.
    /// </summary>
    public class RoleViewModel : ViewModelBase<Guid, Role>
    {
        private readonly IRoleService roleService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IRoleService>();
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IUserAssignementService userAssignmentService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementService>();

        /// <summary>
        /// Default constructor to initialize a new instance of <see cref="RoleViewModel"/>.
        /// </summary>
        /// <param name="role">Instance of <see cref="Role"/>.</param>
        public RoleViewModel(Role role) : base(role)
        {
            this.Model = role;
        }

        /// <summary>
        /// Saves the current instance of the <see cref="RoleViewModel"/> class.
        /// </summary>
        /// <returns>True if the save was succesful; otherwise, false.</returns>
        public bool Save()
        {
            if (Model != null)
            {
                Model.LastManipulation = DateTime.Now;
                if (roleService.Update(Model))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Gets all available active roles from the table.
        /// </summary>
        /// <returns>All avialable active roles from the table.</returns>
        public ObservableCollection<Role> GetAllActiveRoles()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();

            var allRoles = roleService.GetAll();

            if (allRoles != null && allRoles.Count > 0)
            {
                foreach (var role in allRoles)
                {
                    if (role.IsActive)
                    {
                        roles.Add(role);
                    }
                }
            }

            return roles;
        }

        /// <summary>
        /// Retrieves all users assigned to the specified role.
        /// </summary>
        /// <remarks>This method queries the user assignments to find all users associated with the
        /// specified role.  If the role is <see langword="null"/>, an empty collection is returned.</remarks>
        /// <param name="role">The role for which to retrieve the assigned users. Cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="User"/> objects representing the users assigned to
        /// the specified role.  Returns an empty collection if no users are assigned to the role or if the role is <see
        /// langword="null"/>.</returns>
        public ObservableCollection<User> GetAllRoleMember(Role role)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            if(role != null)
            {
                ObservableCollection<UserAssignement> userAssignments = userAssignmentService.GetAll();

                if(userAssignments != null && userAssignments.Count > 0)
                {
                    foreach(var userAssignment in userAssignments)
                    {
                        if (userAssignment.RoleGuid == role.Guid)
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
        /// Adds the specified user to the role associated with the current model.
        /// </summary>
        /// <remarks>This method attempts to assign the specified user to the role represented by the
        /// current model.  Ensure that both the user and the model are valid before calling this method.</remarks>
        /// <param name="user">The user to be added to the role. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the role; otherwise, <see langword="false"/>.</returns>
        public bool AddUserToRole(User user)
        {
            if(user != null && Model != null)
            {
                if(userAssignmentService.AddUserToRole(user.Guid, Model.Guid))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified user from the current role.
        /// </summary>
        /// <remarks>This method checks if the user is assigned to the current role and removes the
        /// assignment if it exists. If the user is not assigned to the role, the method returns <see
        /// langword="false"/>.</remarks>
        /// <param name="user">The user to be removed from the role. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the role; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromRole(User user)
        {
            if (user != null && Model != null)
            {
                var ua = userAssignmentService.GetAll().FirstOrDefault(x => x.UserGuid == user.Guid && x.RoleGuid == Model.Guid);

                if (ua != null)
                {
                    if (userAssignmentService.RemoveUserFromRole(ua))
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }

            return false;
        }

        #region Properties
        /// <summary>
        /// Role unique identifier.
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
        /// Role ident.
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
        /// Role name.
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
        /// Role description.
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
        /// Role is active flag.
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
        /// Role creation date.
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
        /// Role properties last time changed date.
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
