using DCS.DefaultTemplates;

namespace DCS.User
{
    /// <summary>
    /// UserRepository as <see cref="IRepositoryBase{TKey, TModel}"/> to handle user data on the table.
    /// </summary>
    public interface IUserRepository : IRepositoryBase<Guid, User>
    {
        /// <summary>
        /// Gets a user by its given user name.
        /// </summary>
        /// <param name="userName">Given user name.</param>
        /// <returns>User by user name.</returns>
        /// <exception cref="ArgumentNullException">Gets thrown if given UserName is null or empty."</exception>
        User GetByName(string userName);

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
