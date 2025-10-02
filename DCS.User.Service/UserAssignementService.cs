using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents the service for managing user assignments.
    /// </summary>
    public class UserAssignementService : ServiceBase<Guid, UserAssignement, IUserAssignementRepository>, IUserAssignementService
    {
        private readonly IUserAssignementRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignementService"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage user assignments. This parameter cannot be null.</param>
        public UserAssignementService(IUserAssignementRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public bool AddUserToGroup(Guid userGuid, Guid groupGuid)
        {
            var userAssignement = new UserAssignement
            {
                Guid = Guid.NewGuid(),
                UserGuid = userGuid,
                GroupGuid = groupGuid,
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow
            };

            if (repository.New(userAssignement))
                return true;

            return false;
        }

        /// <inheritdoc/>
        public bool RemoveUserFromGroup(UserAssignement userAssignement)
        {
            if(userAssignement.GroupGuid != Guid.Empty)
            {
                if(repository.Delete(userAssignement.Guid))
                    return true;

                return false;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool AddUserToOrganisation(Guid userGuid, Guid organisationGuid)
        {
            var userAssignement = new UserAssignement
            {
                Guid = Guid.NewGuid(),
                UserGuid = userGuid,
                OrganisationGuid = organisationGuid,
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow
            };

            if (repository.New(userAssignement))
                return true;

            return false;
        }

        /// <inheritdoc/>
        public bool RemoveUserFromOrganisation(UserAssignement userAssignement)
        {
            if(userAssignement.OrganisationGuid != Guid.Empty)
            {
                if (repository.Delete(userAssignement.Guid))
                    return true;

                return false;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool AddUserToRole(Guid userGuid, Guid roleGuid)
        {
            var userAssignement = new UserAssignement
            {
                Guid = Guid.NewGuid(),
                UserGuid = userGuid,
                RoleGuid = roleGuid,
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow
            };

            if (repository.New(userAssignement))
                return true;

            return false;
        }

        /// <inheritdoc/>
        public bool RemoveUserFromRole(UserAssignement userAssignement)
        {
            if(userAssignement.RoleGuid != Guid.Empty)
            {
                if (repository.Delete(userAssignement.Guid))
                    return true;

                return false;
            }

            return false;
        }

        /// <inheritdoc/>
        public UserAssignement GetByUserAndGroup(Guid userGuid, Guid groupGuid)
        {
            return repository.GetByUserAndGroup(userGuid, groupGuid);
        }

        /// <inheritdoc/>
        public UserAssignement GetByUserAndOrganisation(Guid userGuid, Guid organisationGuid)
        {
            return repository.GetByUserAndOrganisation(userGuid, organisationGuid);
        }

        /// <inheritdoc/>
        public UserAssignement GetByUserAndRole(Guid userGuid, Guid roleGuid)
        {
            return repository.GetByUserAndRole(userGuid, roleGuid);
        }
    }
}
