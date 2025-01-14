namespace UserLibrary
{
    public class UserService
    {
        private static readonly UserService instance = new UserService();

        public static UserService Singleton
        {
            get { return instance; }
        }

        private readonly User model;

        /// <summary>
        /// UserService contains methods for User-Account management.
        /// </summary>
        private UserService() 
        {
        }

        /// <summary>
        /// Creates new user instance.
        /// </summary>
        /// <returns>User instance.</returns>
        public User CreateUser(User user)
        {
            User newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = user.UserName,
                PassWord = user.PassWord,
                IsAdmin = user.IsAdmin,
                SaveFolderFilePath = user.SaveFolderFilePath,
                ContactDataFilePath = user.ContactDataFilePath,
                CompanyDataFilePath = user.CompanyDataFilePath
            };
            return newUser;
        }
    }
}
