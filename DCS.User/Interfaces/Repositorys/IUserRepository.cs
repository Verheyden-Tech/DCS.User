using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// UserRepository to handle user data on the table.
    /// </summary>
    public interface IUserRepository : IRepositoryBase<Guid, User>
    {
    }
}
