using DCS.DefaultTemplates;

namespace DCS.User
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets user instance by its guid.
        /// </summary>
        User Get(Guid guid);

        /// <summary>
        /// Gets user instance by username.
        /// </summary>
        User GetByName(string userName);

        /// <summary>
        /// Gets all avialable users from the table.
        /// </summary>
        DefaultCollection<User> GetAll();

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

        /// <summary>
        /// Saves a new user instance to the table.
        /// </summary>
        /// <returns>Wether the save was succesful.</returns>
        bool New(User obj);

        /// <summary>
        /// Updates a user instance on the table.
        /// </summary>
        /// <returns>Wether the update was succesful.</returns>
        bool Update(User obj);

        /// <summary>
        /// Deletes a user instance from the table.
        /// </summary>
        /// <returns>Wether the delete was succesful.</returns>
        bool Delete(Guid guid);
    }
}
