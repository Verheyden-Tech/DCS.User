using DCS.DefaultTemplates;
using DCS.User.Helper;

namespace DCS.User.Service
{
    public class UserService : IServiceBase<User, IUserRepository>, IUserService
    {
        private User model;

        public IUserRepository Repository => CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserRepository>();

        public User Model => model;

        public UserService()
        {
            
        }

        public UserService(User user) : this()
        {
            this.model = user;
        }

        public bool Delete(Guid guid)
        {
            return Repository.Delete(guid);
        }

        public User CheckForKeepLoggedIn()
        {
            return Repository.CheckForKeepLoggedIn();
        }

        public bool SetKeepLoggedIn(User user)
        {
            return Repository.SetKeepLoggedIn(user);
        }

        public bool UnsetKeepLoggedIn(User user)
        {
            return Repository.UnsetKeepLoggedIn(user);
        }

        public User Get(Guid guid)
        {
            return Repository.Get(guid);
        }

        public DefaultCollection<User> GetAll()
        {
            return Repository.GetAll();
        }

        public bool New(User obj)
        {
            return Repository.New(obj);
        }

        public bool Update(User obj)
        {
            return Repository.Update(obj);
        }

        public User CreateUser(string userName, string rawPassWord, bool isAdmin, bool keepLoggedIn)
        {
            var hashedPasswort = CryptographyHelper.HashSHA256(rawPassWord);

            var newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = userName,
                PassWord = hashedPasswort,
                IsAdmin = isAdmin,
                CreationDate = DateTime.Now.Date,
                KeepLoggedIn = keepLoggedIn
            };

            return newUser;
        }
    }
}
