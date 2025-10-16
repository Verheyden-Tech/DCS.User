using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Defines a contract for a repository that manages <see cref="UserDomain"/> entities, identified by a <see
    /// cref="Guid"/>.
    /// </summary>
    /// <remarks>This interface extends <see cref="IRepositoryBase{TKey, TEntity}"/> to provide repository
    /// functionality  specific to <see cref="UserDomain"/> entities. It serves as a domain-level abstraction for user-related
    /// data operations.</remarks>
    public interface IUserDomainRepository : IRepositoryBase<Guid, UserDomain>
    {
    }
}
