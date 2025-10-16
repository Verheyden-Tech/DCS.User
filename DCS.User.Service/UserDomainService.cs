using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// Provides domain-specific operations for managing user data, including access and manipulation of user domain
    /// entities.
    /// </summary>
    /// <remarks>This service acts as a bridge between the application layer and the data access layer,
    /// encapsulating business logic  related to user domains. It relies on an <see cref="IUserDomainRepository"/>
    /// implementation to perform data operations.</remarks>
    public class UserDomainService : ServiceBase<Guid, UserDomain, IUserDomainRepository>, IUserDomainService
    {
        private readonly IUserDomainRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserDomainRepository>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class with the specified repository.
        /// </summary>
        /// <param name="repository">The repository used to access and manage user domain data. This parameter cannot be <see langword="null"/>.</param>
        public UserDomainService(IUserDomainRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
