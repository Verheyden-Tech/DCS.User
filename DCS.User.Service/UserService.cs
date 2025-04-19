using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// UserService with basic methods inherited from <see cref="ServiceBase{TKey, TModel, TRepository}"/> to handle user account data.
    /// </summary>
    public class UserService : ServiceBase<Guid, User, IUserRepository>, IUserService
    {
        private readonly IUserRepository repository;

        /// <summary>
        /// Default constructor for UserService.
        /// </summary>
        public UserService(IUserRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public bool RegisterADUser(ADUser aDUser)
        {
            return repository.RegisterADUser(aDUser);
        }

        /// <inheritdoc/>
        public IList<ADUser> GetDomainNames()
        {
            return repository.GetDomainNames();
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
        public User CreateUser(string userName, string rawPassWord, bool isAdmin, bool keepLoggedIn, string adDomain)
        {
            var hashedPasswort = CryptographyHelper.HashSHA256(rawPassWord);

            var fullUserName = !string.IsNullOrEmpty(adDomain) ? $"{adDomain}/{userName}" : userName;

            var newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = fullUserName,
                PassWord = hashedPasswort,
                Domain = adDomain,
                IsAdmin = isAdmin,
                IsActive = true,
                IsADUser = false,
                CreationDate = DateTime.Today,
                KeepLoggedIn = keepLoggedIn,
                LastManipulation = DateTime.Today
            };

            return newUser;
        }

        /// <inheritdoc/>
        public string GetSha256Hash(string rawPassword)
        {
            var hashedPassword = CryptographyHelper.HashSHA256(rawPassword);
            return hashedPassword;
        }

        /// <inheritdoc/>
        public bool LoginUser(string username, string password, string domain)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            var fullUserName = Path.Combine(domain + "/", username);

            //Get user instance by name to check password.
            var user = repository.GetByName(fullUserName);
            string hashedPassword = CryptographyHelper.HashSHA256(password);

            if (hashedPassword == user.PassWord)
            {
                return true;
            }
            return false;
        }
    }
}
