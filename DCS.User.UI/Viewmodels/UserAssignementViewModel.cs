using DCS.CoreLib.BaseClass;
using DCS.CoreLib.Collection;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents a view model for managing user assignments, including operations for adding and removing users from
    /// groups, organizations, and roles, as well as managing assignment properties.
    /// </summary>
    /// <remarks>The <see cref="UserAssignementViewModel"/> class provides functionality to manage user
    /// assignments in various contexts such as groups, organizations, and roles. It includes methods for adding and
    /// removing users from these entities, as well as properties for accessing and modifying assignment details. This
    /// view model is backed by a service layer for data retrieval and persistence.</remarks>
    public class UserAssignementViewModel : ViewModelBase<Guid, UserAssignement>
    {
        private readonly IUserAssignementService service = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignementViewModel"/> class using the specified <see
        /// cref="UserAssignement"/> model.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="ViewModelBase{TKey, TModel}.Model"/> property with the provided
        /// <paramref name="userAssignement"/>  and populates the <see cref="ViewModelBase{TKey, TModel}.Collection"/> property with data retrieved
        /// from the service.</remarks>
        /// <param name="userAssignement">The <see cref="UserAssignement"/> instance that serves as the underlying model for this view model.</param>
        public UserAssignementViewModel(UserAssignement userAssignement) : base(userAssignement)
        {
            this.Model = userAssignement;
            Collection = service.GetAll().Result;
        }

        #region Add/Remove User from Group/Organisation/Role
        /// <summary>
        /// Adds a user to a specified group.
        /// </summary>
        /// <remarks>This method does not throw an exception if the user is already a member of the group
        /// or if the group does not exist. Instead, it returns <see langword="false"/> in such cases.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be added to the group.</param>
        /// <param name="groupGuid">The unique identifier of the group to which the user will be added.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the group; otherwise, <see langword="false"/>.</returns>
        public bool AddUserToGroup(Guid userGuid, Guid groupGuid)
        {
            if(userGuid != Guid.Empty && groupGuid != Guid.Empty)
            {
                try
                {
                    var userAssignement = new UserAssignement
                    {
                        Guid = Guid.NewGuid(),
                        UserGuid = userGuid,
                        GroupGuid = groupGuid,
                        IsActive = true,
                        CreationDate = DateTime.UtcNow,
                        LastManipulation = DateTime.UtcNow,
                        MembershipStart = DateTime.UtcNow
                    };

                    if (service.New(userAssignement).Result)
                        return true;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Failed to add user to group. UserGuid: {userGuid}, GroupGuid: {groupGuid}. Exception: {ex.Message}", $"{ex.Source}");
                    return false;
                }
            }
            
            Log.LogManager.Singleton.Error($"Invalid UserGuid or GroupGuid provided. UserGuid: {userGuid}, GroupGuid: {groupGuid}.", "UserAssignementViewModel");
            return false;
        }

        /// <summary>
        /// Removes a user from a specified group by marking the membership as ended and deleting the associated record.
        /// </summary>
        /// <remarks>This method attempts to find the user's membership in the specified group. If found,
        /// the membership is marked as ended, and the associated record is deleted. If the user is not a member of the
        /// group or an error occurs during the operation, the method logs the issue and returns <see
        /// langword="false"/>.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be removed from the group. Must not be <see cref="Guid.Empty"/>.</param>
        /// <param name="groupGuid">The unique identifier of the group from which the user is to be removed. Must not be <see
        /// cref="Guid.Empty"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the group; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromGroup(Guid userGuid, Guid groupGuid)
        {
            if (userGuid != Guid.Empty && groupGuid != Guid.Empty)
            {
                try
                {
                    var assignement = Collection.FirstOrDefault(x => x.UserGuid == userGuid && x.GroupGuid == groupGuid);
                    if(assignement != null)
                    {
                        assignement.MembershipEnd = DateTime.UtcNow;

                        if (service.Delete(assignement.Guid).Result)
                            return true;
                    }

                    Log.LogManager.Singleton.Error($"User is not a member of the specified group. UserGuid: {userGuid}, GroupGuid: {groupGuid}.", "UserAssignementViewModel");
                    return false;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Failed to remove user from group. UserGuid: {userGuid}, GroupGuid: {groupGuid}. Exception: {ex.Message}", $"{ex.Source}");
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a user to the specified organization.
        /// </summary>
        /// <remarks>This method does not allow duplicate user entries for the same organization. If the
        /// user is already a member of the specified organization, the method will return <see
        /// langword="false"/>.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be added.</param>
        /// <param name="organisationGuid">The unique identifier of the organization to which the user will be added.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the organization; otherwise, <see
        /// langword="false"/>.</returns>
        public bool AddUserToOrganisation(Guid userGuid, Guid organisationGuid)
        {
            var userAssignement = new UserAssignement
            {
                Guid = Guid.NewGuid(),
                UserGuid = userGuid,
                OrganisationGuid = organisationGuid,
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow,
                MembershipStart = DateTime.UtcNow
            };

            if (service.New(userAssignement).Result)
                return true;

            return false;
        }

        /// <summary>
        /// Removes a user from the specified organization.
        /// </summary>
        /// <remarks>This method attempts to remove the user's membership from the specified organization.
        /// If the user is not a member of the organization, or if an error occurs during the operation, the method logs
        /// the issue and returns <see langword="false"/>.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be removed. Must not be <see cref="Guid.Empty"/>.</param>
        /// <param name="organisationGuid">The unique identifier of the organization from which the user is to be removed. Must not be <see
        /// cref="Guid.Empty"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the organization; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromOrganisation(Guid userGuid, Guid organisationGuid)
        {
            if(userGuid != Guid.Empty && organisationGuid != Guid.Empty)
            {
                try
                {
                    var assignement = Collection.FirstOrDefault(x => x.UserGuid == userGuid && x.OrganisationGuid == organisationGuid);
                    if (assignement != null)
                    {
                        assignement.MembershipEnd = DateTime.UtcNow;
                        if (service.Delete(assignement.Guid).Result)
                            return true;
                    }

                    Log.LogManager.Singleton.Error($"User is not a member of the specified organization. UserGuid: {userGuid}, OrganisationGuid: {organisationGuid}.", "UserAssignementViewModel");
                    return false;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Failed to remove user from organization. UserGuid: {userGuid}, OrganisationGuid: {organisationGuid}. Exception: {ex.Message}", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error($"Invalid UserGuid or OrganisationGuid provided. UserGuid: {userGuid}, OrganisationGuid: {organisationGuid}.", "UserAssignementViewModel");
            return false;
        }

        /// <summary>
        /// Adds a user to a specified role.
        /// </summary>
        /// <remarks>This method associates the specified user with the specified role. If the user is
        /// already a member of the role,  the method returns <see langword="false"/> without making any
        /// changes.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be added to the role.</param>
        /// <param name="roleGuid">The unique identifier of the role to which the user will be added.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the role; otherwise, <see langword="false"/>.</returns>
        public bool AddUserToRole(Guid userGuid, Guid roleGuid)
        {
            var userAssignement = new UserAssignement
            {
                Guid = Guid.NewGuid(),
                UserGuid = userGuid,
                RoleGuid = roleGuid,
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow,
                MembershipStart = DateTime.UtcNow
            };

            if (service.New(userAssignement).Result)
                return true;

            return false;
        }

        /// <summary>
        /// Removes a user from a specified role.
        /// </summary>
        /// <remarks>This method attempts to remove the specified user from the specified role. If the
        /// user is not a member of the role,  or if an error occurs during the operation, the method logs the error and
        /// returns <see langword="false"/>.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be removed from the role. Must not be <see cref="Guid.Empty"/>.</param>
        /// <param name="roleGuid">The unique identifier of the role from which the user will be removed. Must not be <see cref="Guid.Empty"/>.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the role; otherwise, <see
        /// langword="false"/>.</returns>
        public bool RemoveUserFromRole(Guid userGuid, Guid roleGuid)
        {
            if (userGuid != Guid.Empty && roleGuid != Guid.Empty)
            {
                try
                {
                    var assignement = Collection.FirstOrDefault(x => x.UserGuid == userGuid && x.RoleGuid == roleGuid);
                    if (assignement != null)
                    {
                        assignement.MembershipEnd = DateTime.UtcNow;
                        if (service.Delete(assignement.Guid).Result)
                            return true;
                    }

                    Log.LogManager.Singleton.Error($"User is not a member of the specified role. UserGuid: {userGuid}, RoleGuid: {roleGuid}.", "UserAssignementViewModel");
                    return false;
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Failed to remove user from role. UserGuid: {userGuid}, RoleGuid: {roleGuid}. Exception: {ex.Message}", $"{ex.Source}");
                    return false;
                }
            }

            Log.LogManager.Singleton.Error($"Invalid UserGuid or RoleGuid provided. UserGuid: {userGuid}, RoleGuid: {roleGuid}.", "UserAssignementViewModel");
            return false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier for the user assignment.
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
        /// Gets or sets the unique identifier for the user.
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
        /// Gets or sets the unique identifier for the group.
        /// </summary>
        public Guid GroupGuid
        {
            get { return Model.GroupGuid; }
            set
            {
                if (Model.GroupGuid != value)
                {
                    Model.GroupGuid = value;
                    OnPropertyChanged(nameof(GroupGuid));
                }
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier for the organization.
        /// </summary>
        public Guid OrganisationGuid
        {
            get { return Model.OrganisationGuid; }
            set
            {
                if (Model.OrganisationGuid != value)
                {
                    Model.OrganisationGuid = value;
                    OnPropertyChanged(nameof(OrganisationGuid));
                }
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier for the role.
        /// </summary>
        public Guid RoleGuid
        {
            get { return Model.RoleGuid; }
            set
            {
                if (Model.RoleGuid != value)
                {
                    Model.RoleGuid = value;
                    OnPropertyChanged(nameof(RoleGuid));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user assignement is currently active.
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
        /// Gets or sets the creation date of the associated model.
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
        /// Gets or sets the timestamp of the most recent modification.
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

        /// <summary>
        /// Gets or sets the start date of the membership.
        /// </summary>
        public DateTime? MembershipStart
        {
            get { return Model.MembershipStart; }
            set
            {
                if (Model.MembershipStart != value)
                {
                    Model.MembershipStart = value;
                    OnPropertyChanged(nameof(MembershipStart));
                }
            }
        }

        /// <summary>
        /// Gets or sets the date and time when the membership ends.
        /// </summary>
        public DateTime? MembershipEnd
        {
            get { return Model.MembershipEnd; }
            set
            {
                if (Model.MembershipEnd != value)
                {
                    Model.MembershipEnd = value;
                    OnPropertyChanged(nameof(MembershipEnd));
                }
            }
        }
        #endregion
    }
}