using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing user assignments on the table.
    /// </summary>
    public interface IUserAssignementRepository : IRepositoryBase<Guid, UserAssignement>
    {
        /// <summary>
        /// Retrieves the user assignment associated with the specified user and group.
        /// </summary>
        /// <param name="userGuid">The unique identifier of the user.</param>
        /// <param name="groupGuid">The unique identifier of the group.</param>
        /// <returns>The <see cref="UserAssignement"/> object representing the assignment between the specified user and group,
        /// or <see langword="null"/> if no such assignment exists.</returns>
        UserAssignement GetByUserAndGroup(Guid userGuid, Guid groupGuid);
        /// <summary>
        /// Retrieves the user assignment associated with the specified user and organization.
        /// </summary>
        /// <param name="userGuid">The unique identifier of the user.</param>
        /// <param name="organisationGuid">The unique identifier of the organization.</param>
        /// <returns>The <see cref="UserAssignement"/> object representing the assignment for the specified user and
        /// organization. Returns <see langword="null"/> if no assignment exists.</returns>
        UserAssignement GetByUserAndOrganisation(Guid userGuid, Guid organisationGuid);
        /// <summary>
        /// Retrieves the user assignment associated with the specified user and role.
        /// </summary>
        /// <param name="userGuid">The unique identifier of the user.</param>
        /// <param name="roleGuid">The unique identifier of the role.</param>
        /// <returns>The <see cref="UserAssignement"/> object representing the assignment of the user to the role,  or <see
        /// langword="null"/> if no such assignment exists.</returns>
        UserAssignement GetByUserAndRole(Guid userGuid, Guid roleGuid);
    }
}
