using DCS.CoreLib;
using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents a user domain and its associated subscription information.
    /// </summary>
    /// <remarks>The UserDomain class encapsulates details about a domain, including its name, subscription
    /// status, license key, and subscription period. This class is typically used to manage and track domain-level
    /// licensing and subscription data within an application.</remarks>
    public class UserDomain : ModelBase, IEntity<Guid>
    {
        /// <summary>
        /// Default constructor for <see cref="UserDomain"/> class.
        /// </summary>
        public UserDomain() { }

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
