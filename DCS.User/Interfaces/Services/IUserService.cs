namespace DCS.User
{
    /// <summary>
    /// UserService to manipulate user data.
    /// </summary>
    public interface IUserService : IUserRepository
    {
        /// <summary>
        /// Gets the Sha256 hash for the user password.
        /// </summary>
        /// <param name="rawPassword">User password.</param>
        /// <returns>Sha256 hashed user password.</returns>
        string GetSha256Hash(string rawPassword);
    }

}
