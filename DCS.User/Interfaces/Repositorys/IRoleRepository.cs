using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository to manage <see cref="Role"/> entities in the database.
    /// </summary>
    public interface IRoleRepository : IRepositoryBase<Guid, Role>
    {
        /// <summary>
        /// Retrieves a role by its name.
        /// </summary>
        /// <param name="roleName">The name of the role to retrieve. This value is case-sensitive and cannot be null or empty.</param>
        /// <returns>The <see cref="Role"/> object that matches the specified name, or <see langword="null"/> if no matching role
        /// is found.</returns>
        public Role GetByName(string roleName);
    }
}
