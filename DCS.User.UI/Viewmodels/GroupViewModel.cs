using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the ViewModel for the <see cref="Group"/> model. Implements the <see cref="ViewModelBase{TKey, TModel}"/> class.
    /// </summary>
    public class GroupViewModel : ViewModelBase<Guid, Group>
    {
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IUserAssignementService userAssignementService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementService>();

        private UserAssignementViewModel assignementViewModel;

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="GroupViewModel"/>.
        /// </summary>
        /// <param name="group">Instance of <see cref="Group"/>.</param>
        public GroupViewModel(Group group) : base(group)
        {
            this.Model = group;

            Collection = groupService.GetAll();

            var ua = new UserAssignement();
            assignementViewModel = new UserAssignementViewModel(ua);
        }

        /// <summary>
        /// Creates a new group based on the current model and adds it to the collection if successful.
        /// </summary>
        /// <remarks>This method initializes a new group using the properties of the current <see
        /// cref="ViewModelBase{TKey, TModel}.Model"/>. The group is then passed to the group service for creation. If the creation is successful,
        /// the group is added to the <see cref="ViewModelBase{TKey, TModel}.Collection"/>. If the model is null or the group service fails to
        /// create the group, an error is logged and the method returns <see langword="false"/>.</remarks>
        /// <returns><see langword="true"/> if the group is successfully created and added to the collection; otherwise, <see
        /// langword="false"/>.</returns>
        public bool CreateNewGroup()
        {
            if (Model != null)
            {
                var newGroup = new Group()
                {
                    Guid = Guid.NewGuid(),
                    Name = Model.Name,
                    Description = Model.Description,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    LastManipulation = DateTime.Now
                };

                if (groupService.New(newGroup))
                {
                    Collection.Add(newGroup);
                    return true;
                }

                Log.LogManager.Singleton.Error("Failed to create new group - service create method returned false.", "GroupViewModel.CreateNewGroup");
                return false;
            }

            Log.LogManager.Singleton.Error("Failed to create new group - model is null.", "GroupViewModel.CreateNewGroup");
            return false;
        }

        /// <summary>
        /// Updates the group information based on the current model.  If the group exists, its details are updated;
        /// otherwise, a new group is created.
        /// </summary>
        /// <remarks>This method attempts to update an existing group identified by the <see
        /// cref="Guid"/>.  If the group does not exist or the <see cref="Guid"/> is not set, a new group is
        /// created  using the <see cref="CreateNewGroup"/> method.  Logs an error if the update or creation process
        /// fails.</remarks>
        /// <returns><see langword="true"/> if the group was successfully updated or created; otherwise, <see langword="false"/>.</returns>
        public bool UpdateGroup()
        {
            if (Model != null && Model.Guid != default)
            {
                var group = groupService.Get(Model.Guid);
                if (group != null)
                {
                    group.Name = Model.Name;
                    group.Description = Model.Description;
                    group.IsActive = Model.IsActive;
                    group.LastManipulation = DateTime.Now;

                    if (groupService.Update(group))
                    {
                        return true;
                    }
                }

                Log.LogManager.Singleton.Error("Failed to update group - group not found.", "GroupViewModel.UpdateGroup");
                return false;
            }
            else
            {
                if (CreateNewGroup())
                    return true;

                Log.LogManager.Singleton.Error("Failed to update group - create new group failed.", "GroupViewModel.UpdateGroup");
                return false;
            }
        }

        /// <summary>
        /// Gets all available active groups from the table.
        /// </summary>
        /// <returns>All avialable active groups from the table.</returns>
        public ObservableCollection<Group> GetAllActiveGroups()
        {
            ObservableCollection<Group> groups = new ObservableCollection<Group>();

            var allGroups = groupService.GetAll();

            if (allGroups != null && allGroups.Count > 0)
            {
                foreach (var group in allGroups)
                {
                    if (group.IsActive)
                    {
                        groups.Add(group);
                    }
                }
            }

            return groups;
        }

        /// <summary>
        /// Retrieves all members of the specified group.
        /// </summary>
        /// <remarks>This method queries the user assignments to determine which users belong to the
        /// specified group. If a user assignment references a user that cannot be retrieved, that user is
        /// skipped.</remarks>
        /// <param name="group">The group for which to retrieve the members. Cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="User"/> objects representing the members of the
        /// specified group.  Returns an empty collection if the group has no members or if the group is <see
        /// langword="null"/>.</returns>
        public ObservableCollection<User> GetAllGroupMember(Group group)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            if (group != null)
            {
                ObservableCollection<UserAssignement> userAssignements = userAssignementService.GetAll();

                if (userAssignements != null && userAssignements.Count > 0)
                {
                    foreach (var userAssignement in userAssignements)
                    {
                        if (userAssignement.GroupGuid == group.Guid)
                        {
                            var user = userService.Get(userAssignement.UserGuid);
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
        /// Adds the specified user to the specified group.
        /// </summary>
        /// <remarks>This method attempts to add the user to the group using the underlying user
        /// assignment service. Ensure that both the user and group objects are valid and initialized before calling
        /// this method.</remarks>
        /// <param name="user">The user to be added to the group. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the group; otherwise, <see langword="false"/>.</returns>
        public bool AddUserToGroup(User user)
        {
            if (user != null && Model != null)
            {
                if (assignementViewModel.AddUserToGroup(user.Guid, Model.Guid))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified user from the specified group.
        /// </summary>
        /// <remarks>This method checks if the user is currently assigned to the group before attempting
        /// to remove them. If either the user or the group is <see langword="null"/>, the method returns <see
        /// langword="false"/>.</remarks>
        /// <param name="user">The user to be removed from the group. Cannot be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the group; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromGroup(User user)
        {
            if (user != null && Model != null)
            {
                if (assignementViewModel.RemoveUserFromGroup(user.Guid, Model.Guid))
                {
                    Collection.Remove(Model);
                    return true;
                }

                Log.LogManager.Singleton.Error($"Failed to remove user {user.UserName} from group {Model.Name}.", "GroupViewModel");
                return false;
            }

            Log.LogManager.Singleton.Error("User model or group is null during removal from group.", "GroupViewModel");
            return false;
        }

        #region Properties
        /// <summary>
        /// Group unique identifier.
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
        /// Group ident.
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
        /// Group name.
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
        /// Group description.
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
        /// Group is active flag.
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
        /// Group creation date.
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
        /// Group properties last time changed date.
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
