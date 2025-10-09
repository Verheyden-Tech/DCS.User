using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// Represents the RoleService for <see cref="Role"/> entities.
    /// </summary>
    public class RoleService : ServiceBase<Guid, Role, IRoleRepository>, IRoleService
    {
        private readonly IRoleRepository repository;

        /// <summary>
        /// Default constructor for RoleService.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IRoleRepository"/>.</param>
        public RoleService(IRoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public Role GetByName(string roleName)
        {
            return repository.GetByName(roleName);
        }

        /// <inheritdoc/>
        public Role CreateRole(string name, string description, bool isActive, Guid userGuid)
        {
            var role = new Role
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Description = description,
                IsActive = isActive,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow
            };

            return role;
        }
    }
}
