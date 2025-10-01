namespace DCS.User
{
    /// <summary>
    /// Represents the GroupService for <see cref="Group"/> entities.
    /// </summary>
    public interface IGroupService : IGroupRepository
    {
        /// <summary>
        /// Creates a new group with the specified name, description, and status, and associates it with the specified
        /// user.
        /// </summary>
        /// <param name="name">The name of the group. This value cannot be null or empty.</param>
        /// <param name="description">A description of the group. This value can be null or empty.</param>
        /// <param name="isActive">A value indicating whether the group is active.</param>
        /// <param name="userGuid">The unique identifier of the user creating the group. This value must not be <see cref="Guid.Empty"/>.</param>
        /// <returns>A <see cref="Group"/> object representing the newly created group.</returns>
        Group CreateGroup(string name, string description, bool isActive, Guid userGuid);
    }
}
