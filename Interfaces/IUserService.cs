using DCS.DefaultTemplates;

namespace DCS.User
{
    /// <summary>
    /// UserService to manipulate user data.
    /// </summary>
    public interface IUserService : IServiceBase<Guid, User, IUserRepository>
    {
        /// <summary>
        /// Gets a user by its given user name.
        /// </summary>
        /// <param name="userName">Given user name.</param>
        /// <returns>User by user name.</returns>
        /// <exception cref="ArgumentNullException">Gets thrown if given UserName is null or empty."</exception>
        User GetByName(string userName);

        /// <summary>
        /// Creates new instance of <see cref="User"/>.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="rawPassWord">User raw password to hash.</param>
        /// <param name="isAdmin">User has admin rights flag.</param>
        /// <param name="keepLoggedIn">User keep logged in flag.</param>
        /// <returns>New instance of <see cref="User"/>.</returns>
        User CreateUser(string userName, string rawPassWord, bool isAdmin, bool keepLoggedIn);

        /// <summary>
        /// Gets the Sha256 hash for the user password.
        /// </summary>
        /// <param name="rawPassword">User password.</param>
        /// <returns>Sha256 hashed user password.</returns>
        string GetSha256Hash(string rawPassword);

        /// <summary>
        /// Login user with username and password.
        /// </summary>
        /// <returns>Wether the login was successful.</returns>
        bool LoginUser(string username, string password);

        /// <summary>
        /// Checks if any user has flag for auto login.
        /// </summary>
        /// <returns>KeepLoggedIn user.</returns>
        User CheckForKeepLoggedIn();

        /// <summary>
        /// Set keep logged in flag for user.
        /// </summary>
        /// <param name="user">User to keep logged in.</param>
        /// <returns>Wether setting the flag was susccesfull.</returns>
        public bool SetKeepLoggedIn(User user);

        /// <summary>
        /// Set keep logged in to flase for given user.
        /// </summary>
        /// <param name="user">User to unset keep logged in.</param>
        /// <returns>Wether setting flag to false was succesfull.</returns>
        public bool UnsetKeepLoggedIn(User user);
    }

}
