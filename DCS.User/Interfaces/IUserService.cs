namespace DCS.User
{
    /// <summary>
    /// UserService to manipulate user data.
    /// </summary>
    public interface IUserService : IUserRepository
    {
        /// <summary>
        /// Creates new instance of <see cref="User"/>.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="rawPassWord">User raw password to hash.</param>
        /// <param name="isAdmin">User has admin rights flag.</param>
        /// <param name="keepLoggedIn">User keep logged in flag.</param>
        /// <param name="adDomain">Active Directory domain name.</param>
        /// <returns>New instance of <see cref="User"/>.</returns>
        User CreateUser(string userName, string rawPassWord, bool isAdmin, bool keepLoggedIn, string adDomain);

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
        bool LoginUser(string username, string password, string domain);
    }

}
