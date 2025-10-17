using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// Represents the GroupService for <see cref="Group"/> entities.
    /// </summary>
    public class GroupService : ServiceBase<Guid, Group, IGroupRepository>, IGroupService
    {
        private readonly IGroupRepository repository;

        /// <summary>
        /// Default constructor initialize a new instance of the <see cref="GroupService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IGroupRepository"/>.</param>
        public GroupService(IGroupRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
