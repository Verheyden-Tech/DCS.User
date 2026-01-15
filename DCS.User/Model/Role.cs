using DCS.CoreLib;
using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents a user role within the system, including its identifier, description, and audit information.
    /// </summary>
    public class Role : ModelBase, IEntity<Guid>
    {
        /// <summary>
        /// Default constructor for <see cref="Role"/> instances.
        /// </summary>
        public Role() { }

        /// <summary>
        /// Role ident.
        /// </summary>
        public int Ident { get; set; }

        /// <summary>
        /// Role description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Role creation date.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Role properties last time changed date.
        /// </summary>
        public DateTime? LastManipulation { get; set; }
    }
}
