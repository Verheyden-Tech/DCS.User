namespace DCS.User
{
    /// <summary>
    /// Represents the user role class.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Default constructor for <see cref="Role"/> instances.
        /// </summary>
        public Role() { }

        /// <summary>
        /// Role unique identifier.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Role ident.
        /// </summary>
        public int Ident { get; set; }

        /// <summary>
        /// Role name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Role is active flag.
        /// </summary>
        public bool IsActive { get; set; }

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
