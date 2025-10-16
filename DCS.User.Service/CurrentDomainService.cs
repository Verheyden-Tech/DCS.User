namespace DCS.User.Service
{
    /// <summary>
    /// Provides a thread-safe service for managing the current user domain within the application.
    /// </summary>
    /// <remarks>The <see cref="CurrentDomainService"/> class is implemented as a singleton, ensuring that
    /// only one instance of the service exists throughout the application's lifetime. It allows setting, retrieving,
    /// and resetting the current user domain in a thread-safe manner. Once the domain is set, it becomes immutable
    /// until explicitly reset using the <see cref="UnsetDomain"/> method.</remarks>
    public class CurrentDomainService
    {
        private static readonly Lazy<CurrentDomainService> _instance = new(() => new CurrentDomainService());
        private static readonly object _lock = new();
        private bool isLocked;

        /// <summary>
        /// Gets the singleton instance of the <see cref="CurrentDomainService"/> class.
        /// </summary>
        public static CurrentDomainService Instance => _instance.Value;

        /// <summary>
        /// Represents the current domain associated with the user.
        /// </summary>
        /// <remarks>This field holds the user's domain information and is used internally to manage
        /// user-specific operations.</remarks>
        private UserDomain currentDomain;

        /// <summary>
        /// Gets the current user domain associated with the application.
        /// </summary>
        /// <remarks>The property is thread-safe and ensures that the domain is set only once.  Attempts
        /// to modify the value after it has been set will be ignored, and an error will be logged.</remarks>
        public UserDomain CurrentDomain
        {
            get
            {
                lock (_lock)
                {
                    return currentDomain;
                }
            }
            private set
            {
                lock (_lock)
                {
                    if (!isLocked)
                    {
                        currentDomain = value;
                        isLocked = true;
                    }
                    else
                    {
                        Log.LogManager.Singleton.Error("Current domain is already set and cannot be changed.", "Setting current domain");
                    }
                }
            }
        }

        /// <summary>
        /// Sets the current domain for the user.
        /// </summary>
        /// <remarks>This method updates the <c>CurrentDomain</c> property to the specified domain. Ensure
        /// that the provided domain is valid and not <see langword="null"/>.</remarks>
        /// <param name="domain">The <see cref="UserDomain"/> to set as the current domain. Cannot be <see langword="null"/>.</param>
        public void SetDomain(UserDomain domain)
        {
            CurrentDomain = domain;
        }

        /// <summary>
        /// Resets the current domain to its default state.
        /// </summary>
        /// <remarks>This method clears the current domain by setting it to <see langword="null"/> and
        /// unlocks the domain by setting the lock state to <see langword="false"/>. It is thread-safe and ensures that
        /// the operation is performed atomically.</remarks>
        public void UnsetDomain()
        {
            lock (_lock)
            {
                currentDomain = null;
                isLocked = false;
            }
        }
    }
}
