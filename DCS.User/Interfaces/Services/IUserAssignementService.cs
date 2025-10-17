using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the service for managing user assignments.
    /// </summary>
    public interface IUserAssignementService : IServiceBase<Guid, UserAssignement, IUserAssignementRepository>
    {
    }
}
