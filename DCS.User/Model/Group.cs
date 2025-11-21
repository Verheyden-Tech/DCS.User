using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents the user group class.
    /// </summary>
    public class Group : ModelBase<Guid>
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
