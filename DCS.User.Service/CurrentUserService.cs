namespace DCS.User.Service
{
    /// <summary>
    /// Provides a thread-safe service for managing the current user of the application.
    /// </summary>
    /// <remarks>This class implements a singleton pattern to ensure a single instance is used throughout the
    /// application. It allows setting, retrieving, and clearing the current user in a thread-safe manner. Once the
    /// current user is set, it cannot be modified unless explicitly cleared using the <see cref="UnsetUser"/>
    /// method.</remarks>
    public class CurrentUserService
    {
        private static readonly Lazy<CurrentUserService> _instance = new(() => new CurrentUserService());
        private static readonly object _lock = new();
        private bool isLocked;

        /// <summary>
        /// Gets the singleton instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        public static CurrentUserService Instance => _instance.Value;

        /// <summary>
        /// Represents the currently logged-in user.
        /// </summary>
        /// <remarks>This field holds the user information for the active session. It is intended for
        /// internal use  and should not be accessed directly outside of the class. Use appropriate methods or
        /// properties  to interact with the current user.</remarks>
        private User currentUser;

        /// <summary>
        /// Gets the current user of the application.
        /// </summary>
        /// <remarks>The property is thread-safe and ensures that the user is set only once. Any attempt
        /// to modify the user after it has been set will be logged as an error.</remarks>
        public User CurrentUser
        {
            get
            {
                lock (_lock)
                {
                    return currentUser;
                }
            }
            private set
            {
                lock (_lock)
                {
                    if (!isLocked)
                    {
                        currentUser = value;
                        isLocked = true;
                    }
                    else
                    {
                        Log.LogManager.Singleton.Error("Current user is already set and cannot be changed.", "Setting current user");
                    }
                }
            }
        }

        /// <summary>
        /// Sets the current user of the application.
        /// </summary>
        /// <param name="user">The user to set as the current user. Cannot be <see langword="null"/>.</param>
        public void SetUser(User user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// Clears the current user and unlocks the user state.
        /// </summary>
        /// <remarks>This method sets the current user to <see langword="null"/> and resets the lock state
        /// to allow further modifications. It is thread-safe and ensures that the operation is performed
        /// atomically.</remarks>
        public void UnsetUser()
        {
            lock (_lock)
            {
                currentUser = null;
                isLocked = false;
            }
        }
    }
}
