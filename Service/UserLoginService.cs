using DCS.DefaultTemplates;
using DCS.DBManager;
using DCS.User.Helper;

namespace DCS.User.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class UserLoginService : IUserLoginService
    {
        private DefaultCollection<User> _users;
        private IUserRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserRepository>();
        private IDataBaseManager dataBaseManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IDataBaseManager>();

        public UserLoginService()
        {
            _users = new DefaultCollection<User>();
        }

        public UserLoginService(string connectionString) : this()
        {
            repository = new UserRepository(connectionString);
        }

        /// <inheritdoc/>
        public DefaultCollection<User> LoadUsers()
        {
            if(_users == null)
            {
                _users = new DefaultCollection<User>();
            }

            //Load all avialable users from DB.
            return _users = repository.GetAll();
        }

        /// <inheritdoc/>
        public User GetUser(Guid guid)
        {
            return repository.Get(guid);
        }

        /// <inheritdoc/>
        public User GetUserByName(string userName)
        {
            return repository.GetByName(userName);
        }

        /// <inheritdoc/>
        public bool LoginUser(string username, string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return false;
            }

            //Get user instance by name to check password.
            var user = repository.GetByName(username);
            string hashedPassword = CryptographyHelper.HashSHA256(password);
            if(hashedPassword == user.PassWord)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool TestConnection(string connectionString)
        {
            if(dataBaseManager.TestConnection(connectionString))
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public string GetSha256Hash(string rawPassword)
        {
            var hashedPassword = CryptographyHelper.HashSHA256(rawPassword);
            return hashedPassword;
        }

        /// <summary>
        /// Contains the loaded users from the DB.
        /// </summary>
        public DefaultCollection<User> Users
        {
            get => _users;
            set => _users = value;
        }
    }
}
