using DCS.DefaultTemplates;

namespace DCS.User
{
    /// <summary>
    /// UserService to manipulate user data.
    /// </summary>
    public interface IUserService : IServiceBase<User, IUserRepository>
    {
        /// <summary>
        /// Creates new instance of <see cref="User"/>.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="rawPassWord">User raw password to hash.</param>
        /// <param name="isAdmin">User is active flag.</param>
        /// <param name="isLocalUser">User is local user flag.</param>
        /// <returns>New instance of <see cref="User"/>.</returns>
        User CreateUser(string userName, string rawPassWord, bool isAdmin, bool keepLoggedIn);

        /// <inheritdoc/>
        User CheckForKeepLoggedIn();

        /// <inheritdoc/>
        bool SetKeepLoggedIn(User user);

        /// <inheritdoc/>
        bool UnsetKeepLoggedIn(User user);
    }
}
