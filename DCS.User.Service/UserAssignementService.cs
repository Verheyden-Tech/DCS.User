using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents the service for managing user assignments.
    /// </summary>
    public class UserAssignementService : ServiceBase<Guid, UserAssignement, IUserAssignementRepository>, IUserAssignementService
    {
        private readonly IUserAssignementRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserAssignementRepository>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignementService"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage user assignments. This parameter cannot be null.</param>
        public UserAssignementService(IUserAssignementRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
