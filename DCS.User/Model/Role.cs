using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents the user role class.
    /// </summary>
    public class Role : ModelBase<Guid>
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
