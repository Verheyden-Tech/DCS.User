namespace DCS.User
{
    /// <summary>
    /// Represents the service for managing user assignments.
    /// </summary>
    public interface IUserAssignementService : IUserAssignementRepository
    {
        /// <summary>
        /// Adds a user to a specified group.
        /// </summary>
        /// <remarks>This method does not throw an exception if the user is already a member of the group
        /// or if the group does not exist. Instead, it returns <see langword="false"/> in such cases.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be added to the group.</param>
        /// <param name="groupGuid">The unique identifier of the group to which the user will be added.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the group; otherwise, <see langword="false"/>.</returns>
        bool AddUserToGroup(Guid userGuid, Guid groupGuid);

        /// <summary>
        /// Removes a user from the specified group.
        /// </summary>
        /// <remarks>This method will return <see langword="false"/> if the user is not a member of the
        /// specified group.</remarks>
        /// <param name="userAssignement">An object containing the user and group information for the removal operation.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the group; otherwise, <see
        /// langword="false"/>.</returns>
        bool RemoveUserFromGroup(UserAssignement userAssignement);

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
        bool AddUserToOrganisation(Guid userGuid, Guid organisationGuid);

        /// <summary>
        /// Removes a user from the specified organization.
        /// </summary>
        /// <remarks>The operation will fail if the user is not currently assigned to the specified
        /// organization. Ensure that the <paramref name="userAssignement"/> parameter is not <see langword="null"/> and
        /// contains valid data.</remarks>
        /// <param name="userAssignement">An object containing the details of the user and the organization from which the user should be removed.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the organization; otherwise, <see
        /// langword="false"/>.</returns>
        bool RemoveUserFromOrganisation(UserAssignement userAssignement);

        /// <summary>
        /// Adds a user to a specified role.
        /// </summary>
        /// <remarks>This method associates the specified user with the specified role. If the user is
        /// already a member of the role,  the method returns <see langword="false"/> without making any
        /// changes.</remarks>
        /// <param name="userGuid">The unique identifier of the user to be added to the role.</param>
        /// <param name="roleGuid">The unique identifier of the role to which the user will be added.</param>
        /// <returns><see langword="true"/> if the user was successfully added to the role; otherwise, <see langword="false"/>.</returns>
        bool AddUserToRole(Guid userGuid, Guid roleGuid);

        /// <summary>
        /// Removes a user from the specified role.
        /// </summary>
        /// <remarks>This method does not throw an exception if the user is not currently assigned to the
        /// specified role. Ensure that the <paramref name="userAssignement"/> parameter is not <see langword="null"/>
        /// and contains valid user and role data.</remarks>
        /// <param name="userAssignement">An object containing the user and role information to be removed.</param>
        /// <returns><see langword="true"/> if the user was successfully removed from the role; otherwise, <see
        /// langword="false"/>.</returns>
        bool RemoveUserFromRole(UserAssignement userAssignement);
    }
}
