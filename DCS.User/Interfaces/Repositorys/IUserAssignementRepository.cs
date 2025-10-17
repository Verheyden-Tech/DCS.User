using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing user assignments on the table.
    /// </summary>
    public interface IUserAssignementRepository : IRepositoryBase<Guid, UserAssignement>
    {
    }
}
