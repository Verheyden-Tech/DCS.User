namespace DCS.User
{
    public class CurrentUserService
    {
        private static readonly Lazy<CurrentUserService> _instance = new(() => new CurrentUserService());
        private static readonly object _lock = new();
        private bool isLocked = false;

        public static CurrentUserService Instance => _instance.Value;

        private User currentUser;

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
                        LogManager.LogManager.Singleton.Error("Current user is already set and cannot be changed.", "Setting current user");
                    }
                }
            }
        }

        public void SetUser(User user)
        {
            CurrentUser = user;
        }
    }
}
