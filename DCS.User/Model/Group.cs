using DCS.CoreLib;
using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents a group entity with identifying information, description, and audit metadata.
    /// </summary>
    public class Group : ModelBase, IEntity<Guid>
    {
        /// <summary>
        /// Default constructor for <see cref="Group"/> instances.
        /// </summary>
        public Group() { }

        /// <summary>
        /// Group ident.
        /// </summary>
        public int Ident { get; set; }

        /// <summary>
        /// Group description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Group creation date.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Group properties last time changed date.
        /// </summary>
        public DateTime? LastManipulation { get; set; }
    }
}
