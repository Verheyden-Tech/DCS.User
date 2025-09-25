using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing <see cref="Group"/> entities in the database.
    /// </summary>
    public interface IGroupRepository : IRepositoryBase<Guid, Group>
    {
    }
}
