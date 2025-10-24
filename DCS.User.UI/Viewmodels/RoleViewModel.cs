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

        private UserAssignementViewModel assignementViewModel;

        /// <summary>
        /// Default constructor to initialize a new instance of <see cref="RoleViewModel"/>.
        /// </summary>
        /// <param name="role">Instance of <see cref="Role"/>.</param>
        public RoleViewModel(Role role) : base(role)
        {
            this.Model = role;

            Collection = roleService.GetAll();

            var ua = new UserAssignement();
            assignementViewModel = new UserAssignementViewModel(ua);
        }

        /// <summary>
        /// Creates a new role based on the current model and adds it to the collection or persists it using the role
        /// service.
        /// </summary>
        /// <remarks>This method initializes a new <see cref="Role"/> object using the properties of the
        /// current model. The role is marked as active and timestamps for creation and last manipulation are set to the
        /// current time. If the role service successfully persists the new role, the method returns <see
        /// langword="true"/>. Otherwise, the role is added to the collection. Logs an error if the model is <see
        /// langword="null"/> or if an exception occurs during the operation.</remarks>
        /// <returns><see langword="true"/> if the role is successfully created and persisted; otherwise, <see
        /// langword="false"/>.</returns>
        public bool CreateNewRole()
        {
            if(Model != null)
            {
                try
                {
                    var newRole = new Role
                    {
                        Guid = Guid.NewGuid(),
                        Name = Model.Name,
                        Description = Model.Description,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                        LastManipulation = DateTime.Now
                    };

                    if(roleService.New(newRole))
                        return true;

                    Collection.Add(newRole);
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Error while creating new role: {ex.Message}.", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error("Role model is null.", "RoleViewModel.CreateNewRole");
            return false;
        }

        /// <summary>
        /// Updates the role associated with the current model. If the role does not exist, attempts to create a new
        /// role.
        /// </summary>
        /// <remarks>This method retrieves the role using the GUID from the current model. If the role
        /// exists, it updates the role with the latest information from the model. If the role does not exist, it
        /// attempts to create a new role by invoking the  <see cref="CreateNewRole"/> method. Any errors encountered
        /// during the update or creation process are logged.</remarks>
        /// <returns><see langword="true"/> if the role was successfully updated or created; otherwise, <see langword="false"/>.</returns>
        public bool UpdateRole()
        {
            if (Model != null && Model.Guid != default)
            {
                var role = roleService.Get(Model.Guid);
                if(role != null)
                {
                    try
                    {
                        Model.LastManipulation = DateTime.Now;

                        if (roleService.Update(Model))
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogManager.Singleton.Error($"Error while updating role: {ex.Message}.", $"{ex.Source}");
                        return false;
                    }
                }
                
                Log.LogManager.Singleton.Error($"Role with GUID {Model.Guid} not found.", "RoleViewModel.UpdateRole");
                return false;
            }
            else
            {
                if (CreateNewRole())
                    return true;
            }

            Log.LogManager.Singleton.Error("Role model is null.", "RoleViewModel.UpdateRole");
            return false;
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

            if (role != null)
            {
                ObservableCollection<UserAssignement> userAssignments = userAssignmentService.GetAll();

                if (userAssignments != null && userAssignments.Count > 0)
                {
                    foreach (var userAssignment in userAssignments)
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
            if (user != null && Model != null)
            {
                if (assignementViewModel.AddUserToRole(user.Guid, Model.Guid))
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
                if (assignementViewModel.RemoveUserFromRole(user.Guid, Model.Guid))
                {
                    Collection.Remove(Model);
                    return true;
                }

                Log.LogManager.Singleton.Error($"User with GUID {user.Guid} is not assigned to role with GUID {Model.Guid}.", "RoleViewModel.RemoveUserFromRole");
                return false;
            }

            Log.LogManager.Singleton.Error("User or Role model is null.", "RoleViewModel.RemoveUserFromRole");
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
