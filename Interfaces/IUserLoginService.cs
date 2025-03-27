using DCS.DefaultTemplates;
using System.Collections.ObjectModel;

namespace DCS.User
{
    /// <summary>
    /// UserLoginService to manage the user login.
    /// </summary>
    public interface IUserLoginService
    {
        /// <summary>
        /// Load all avialable users from the table.
        /// </summary>
        /// <returns>Collection of users.</returns>
        DefaultCollection<User> LoadUsers();

        /// <summary>
        /// Gets user instance from the table by its guid.
        /// </summary>
        User GetUser(Guid guid);

        /// <summary>
        /// Gets user instance from the table by its username.
        /// </summary>
        User GetUserByName(string userName);

        /// <summary>
        /// Login user with username and password.
        /// </summary>
        /// <returns>Wether the login was successful.</returns>
        bool LoginUser(string username, string password);

        /// <summary>
        /// Test the DB connection.
        /// </summary>
        /// <returns>Wether the connection was succesful.</returns>
        bool TestConnection(string connectionString);

        /// <summary>
        /// Gets the Sha256 hash for the user password.
        /// </summary>
        /// <param name="rawPassword">User password.</param>
        /// <returns>Sha256 hashed user password.</returns>
        string GetSha256Hash(string rawPassword);
    }
}
