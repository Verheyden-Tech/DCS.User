using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing <see cref="Organisation"/> entities in the database.
    /// </summary>
    public interface IOrganisationRepository : IRepositoryBase<Guid, Organisation>
    {
        /// <summary>
        /// Gets an organisation by its name.
        /// </summary>
        /// <param name="organisationName">The name of the organisation.</param>
        /// <returns>The organisation with the specified name, or null if not found.</returns>
        Organisation GetByName(string organisationName);
    }
}
