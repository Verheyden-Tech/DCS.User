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

        /// <inheritdoc/>
        public Group CreateGroup(string name, string description, bool isActive, Guid userGuid)
        {
            var group = new Group
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Description = description,
                IsActive = isActive,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow
            };
            
            return group;
        }

        /// <inheritdoc/>
        public Group GetByName(string groupName)
        {
            return repository.GetByName(groupName);
        }
    }
}
