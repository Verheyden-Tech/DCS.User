using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// UserService with basic methods inherited from <see cref="ServiceBase{TKey, TModel, TRepository}"/> to handle user account data.
    /// </summary>
    public class UserService : ServiceBase<Guid, User, IUserRepository>, IUserService
    {
        private readonly IUserRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserRepository>();

        /// <summary>
        /// Default constructor for UserService.
        /// </summary>
        public UserService(IUserRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public User GetByName(string userName)
        {
            return repository.GetByName(userName);
        }

        /// <inheritdoc/>
        public User CheckForKeepLoggedIn()
        {
            return repository.CheckForKeepLoggedIn();
        }

        /// <inheritdoc/>
        public bool SetKeepLoggedIn(User user)
        {
            return repository.SetKeepLoggedIn(user);
        }

        /// <inheritdoc/>
        public bool UnsetKeepLoggedIn(User user)
        {
            return repository.UnsetKeepLoggedIn(user);
        }

        /// <inheritdoc/>
        public string GetSha256Hash(string rawPassword)
        {
            var hashedPassword = CryptographyHelper.HashSHA256(rawPassword);
            return hashedPassword;
        }
    }
}
