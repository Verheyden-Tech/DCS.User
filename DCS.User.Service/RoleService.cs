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
    }
}
