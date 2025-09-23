namespace DCS.User.Model
{
    /// <summary>
    /// Represents the user group class.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Default constructor for <see cref="Group"/> instances.
        /// </summary>
        public Group() { }

        /// <summary>
        /// Group unique identifier.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Group ident.
        /// </summary>
        public int Ident { get; set; }

        /// <summary>
        /// Group name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Group description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Group is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Group creator user unique identifier.
        /// </summary>
        public Guid UserGuid { get; set; }

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
