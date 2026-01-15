using DCS.CoreLib;
using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents a user account with authentication, profile, and administrative properties.
    /// </summary>
    /// <remarks>The User class encapsulates information related to user identity, authentication, and profile
    /// preferences. It includes properties for user credentials, domain association, administrative rights, and profile
    /// customization. This class can be used to manage user accounts in authentication and authorization scenarios, as
    /// well as to store user-specific settings such as language and profile picture. Thread safety is not guaranteed;
    /// if instances are shared across threads, callers should implement appropriate synchronization.</remarks>
    public class User : ModelBase, IEntity<Guid>
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
