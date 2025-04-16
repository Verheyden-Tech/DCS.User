namespace DCS.User
{
    /// <summary>
    /// Represents the user class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Default constructor for <see cref="User"/> instances.
        /// </summary>
        public User() { }

        /// <summary>
        /// User unique identifier.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// User domain name.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// User is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// User keep logged in flag.
        /// </summary>
        public bool KeepLoggedIn { get; set; }

        /// <summary>
        /// User has admin rights flag.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// User is logged in in current session flag.
        /// </summary>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// User is local user flag.
        /// </summary>
        public bool IsADUser { get; set; }

        /// <summary>
        /// User creation date.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// User substituition ending date.
        /// </summary>
        public DateTime? SubstitutionEnd { get; set; }

        /// <summary>
        /// User last time properties changed date.
        /// </summary>
        public DateTime? LastManipulation {  get; set; }
    }
}
