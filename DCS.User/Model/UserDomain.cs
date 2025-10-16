namespace DCS.User
{
    /// <summary>
    /// Model for Active Directory user.
    /// </summary>
    public class UserDomain
    {
        /// <summary>
        /// Default constructor for <see cref="UserDomain"/> class.
        /// </summary>
        public UserDomain() { }

        /// <summary>
        /// Gets or sets the unique identifier for the domain.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Indicates whether the domain has an active subscription.
        /// </summary>
        public bool SubscriptionActive { get; set; }

        /// <summary>
        /// Gets or sets the license key used to activate the application or service.
        /// </summary>
        public Guid LicenceKey { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the subscription for the domain starts.
        /// </summary>
        public DateTime StartSubscription { get; set; }

        /// <summary>
        /// Gets or sets the estimated date and time when the subscription ends.
        /// </summary>
        public DateTime? EndSubscription { get; set; }
    }
}
