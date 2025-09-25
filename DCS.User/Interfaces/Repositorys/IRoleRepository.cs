using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository to manage <see cref="Role"/> entities in the database.
    /// </summary>
    public interface IRoleRepository : IRepositoryBase<Guid, Role>
    {
    }
}
