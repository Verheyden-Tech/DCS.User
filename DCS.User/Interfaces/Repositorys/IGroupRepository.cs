using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing <see cref="Group"/> entities in the database.
    /// </summary>
    public interface IGroupRepository : IRepositoryBase<Guid, Group>
    {
        /// <summary>
        /// Retrieves a group by its name.
        /// </summary>
        /// <param name="groupName">The name of the group to retrieve. This value cannot be null or empty.</param>
        /// <returns>The <see cref="Group"/> object that matches the specified name, or <see langword="null"/> if no group with
        /// the given name exists.</returns>
        Group GetByName(string groupName);
    }
}
