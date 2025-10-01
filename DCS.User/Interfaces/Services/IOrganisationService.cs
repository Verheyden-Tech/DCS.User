namespace DCS.User
{
    /// <summary>
    /// Represents the organisation service with basic methods inherited from <see cref="IOrganisationRepository"/> to handle organisation data.
    /// </summary>
    public interface IOrganisationService : IOrganisationRepository
    {
        /// <summary>
        /// Creates a new organisation with the specified name, description, and status.
        /// </summary>
        /// <param name="name">The name of the organisation. Cannot be null or empty.</param>
        /// <param name="description">A brief description of the organisation. Can be null or empty.</param>
        /// <param name="isActive">A value indicating whether the organisation is active.</param>
        /// <param name="userGuid">The unique identifier of the user creating the organisation. Must be a valid <see cref="Guid"/>.</param>
        /// <returns>An <see cref="Organisation"/> object representing the newly created organisation.</returns>
        Organisation CreateOrganisation(string name, string description, bool isActive, Guid userGuid);
    }
}
