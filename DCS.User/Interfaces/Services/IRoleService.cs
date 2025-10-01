using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the service for managing <see cref="Role"/> entities.
    /// </summary>
    public interface IRoleService : IServiceBase<Guid, Role, IRoleRepository>
    {
        /// <summary>
        /// Creates a new role with the specified name, description, and status, and associates it with the specified
        /// user.
        /// </summary>
        /// <param name="name">The name of the role. This value cannot be null or empty.</param>
        /// <param name="description">A description of the role. This value can be null or empty.</param>
        /// <param name="isActive">A value indicating whether the role is active.</param>
        /// <param name="userGuid">The unique identifier of the user associated with the role.</param>
        /// <returns>The newly created <see cref="Role"/> instance.</returns>
        Role CreateRole(string name, string description, bool isActive, Guid userGuid);
    }
}
