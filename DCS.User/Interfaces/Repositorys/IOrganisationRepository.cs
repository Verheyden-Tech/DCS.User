using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing <see cref="Organisation"/> entities in the database.
    /// </summary>
    public interface IOrganisationRepository : IRepositoryBase<Guid, Organisation>
    {
    }
}
