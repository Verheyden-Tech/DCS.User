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

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="GroupViewModel"/>.
        /// </summary>
        /// <param name="group">Instance of <see cref="Group"/>.</param>
        public GroupViewModel(Group group) : base(group)
        {
            this.Model = group;
        }

        /// <summary>
        /// Saves the current instance of the <see cref="GroupViewModel"/> class.
        /// </summary>
        /// <returns>True if the save was successful; otherwise, false.</returns>
        public bool Save()
        {
            if (Model != null)
            {
                Model.LastManipulation = DateTime.Now;
                if (groupService.Update(Model))
                {
                    return true;
                }
                return false;
            }
            return false;
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
                    if(group.IsActive)
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
            
            if(group != null)
            {
                ObservableCollection<UserAssignement> userAssignements = userAssignementService.GetAll();

                if(userAssignements != null && userAssignements.Count > 0)
                {
                    foreach(var userAssignement in userAssignements)
                    {
                        if(userAssignement.GroupGuid == group.Guid)
                        {
                            var user = userService.Get(userAssignement.UserGuid);
                            if(user != null)
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
            if(user != null && Model != null)
            {
                if(userAssignementService.AddUserToGroup(user.Guid, Model.Guid))
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
                var ua = userAssignementService.GetAll().FirstOrDefault(x => x.UserGuid == user.Guid && x.GroupGuid == Model.Guid);

                if (ua != null)
                {
                    if(userAssignementService.RemoveUserFromGroup(ua))
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
        /// Group creator user unique identifier.
        /// </summary>
        public Guid UserGuid
        {
            get { return Model.UserGuid; }
            set
            {
                if (Model.UserGuid != value)
                {
                    Model.UserGuid = value;
                    OnPropertyChanged(nameof(UserGuid));
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
