namespace DCS.User
{
    /// <summary>
    /// Represents the user organisation class.
    /// </summary>
    public class Organisation
    {
        /// <summary>
        /// Default constructor for <see cref="Organisation"/> instances.
        /// </summary>
        public Organisation() { }

        /// <summary>
        /// Organisation unique identifier.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Organisation ident.
        /// </summary>
        public int Ident { get; set; }

        /// <summary>
        /// Organisation name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Organisation description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Organisation is active flag.
        /// </summary>
        public bool IsActive { get; set; }

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
