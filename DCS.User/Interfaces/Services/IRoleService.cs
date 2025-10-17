using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the service for managing <see cref="Role"/> entities.
    /// </summary>
    public interface IRoleService : IServiceBase<Guid, Role, IRoleRepository>
    {
    }
}
