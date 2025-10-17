using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the organisation service with basic methods inherited from <see cref="IOrganisationRepository"/> to handle organisation data.
    /// </summary>
    public interface IOrganisationService : IServiceBase<Guid, Organisation, IOrganisationRepository>
    {
    }
}
