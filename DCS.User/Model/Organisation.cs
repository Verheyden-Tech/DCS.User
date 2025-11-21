using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents the user organisation class.
    /// </summary>
    public class Organisation : ModelBase<Guid>
    {
        /// <summary>
        /// Default constructor for <see cref="Organisation"/> instances.
        /// </summary>
        public Organisation() { }

        /// <summary>
        /// Organisation ident.
        /// </summary>
        public int Ident { get; set; }

        /// <summary>
        /// Organisation description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Organisation creation date.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Organisation properties last time changed date.
        /// </summary>
        public DateTime? LastManipulation { get; set; }
    }
}
