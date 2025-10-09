namespace DCS.User
{
    /// <summary>
    /// Model for Active Directory user.
    /// </summary>
    public class ADUser
    {
        /// <summary>
        /// Default constructor for <see cref="ADUser"/> class.
        /// </summary>
        public ADUser() { }

        /// <summary>
        /// Gets or sets the unique identifier for the domain.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name of the user domain.
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Indicates whether the user is an active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
