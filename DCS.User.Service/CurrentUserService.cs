namespace DCS.User.Service
{
    /// <summary>
    /// CurrentUserService to set and get the current logged in user.
    /// </summary>
    public class CurrentUserService
    {
        private static readonly Lazy<CurrentUserService> _instance = new(() => new CurrentUserService());
        private static readonly object _lock = new();
        private bool isLocked;

        /// <summary>
        /// Gets the lazily initialized value for current user instance.
        /// </summary>
        public static CurrentUserService Instance => _instance.Value;

        private User currentUser;

        /// <summary>
        /// Gets or sets the current logged in user and lock it after first set for the session.
        /// </summary>
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
        /// Sets the given user as current logged in user.
        /// </summary>
        /// <param name="user">Current user.</param>
        public void SetUser(User user)
        {
            CurrentUser = user;
        }
    }
}
