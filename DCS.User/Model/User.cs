using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents the user class.
    /// </summary>
    public class User : ModelBase<Guid>
    {
        /// <summary>
        /// Default constructor for <see cref="User"/> instances.
        /// </summary>
        public User() { }

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
        /// User keep logged in flag.
        /// </summary>
        public bool KeepLoggedIn { get; set; }

        /// <summary>
        /// User has admin rights flag.
        /// </summary>
        public bool IsAdmin { get; set; }

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
        /// User properties last time changed date.
        /// </summary>
        public DateTime? LastManipulation {  get; set; }

        /// <summary>
        /// Gets or sets the path to the user's profile picture.
        /// </summary>
        public string? ProfilePicturePath { get; set; }

        /// <summary>
        /// Gets or sets the prefered language for the user.
        /// </summary>
        public string Language { get; set; }
    }
}
