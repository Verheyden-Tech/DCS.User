using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// Represents the OrganisationService for <see cref="Organisation"/> entities.
    /// </summary>
    public class OrganisationService : ServiceBase<Guid, Organisation, IOrganisationRepository>, IOrganisationService
    {
        private readonly IOrganisationRepository repository;

        /// <summary>
        /// Default constructor for OrganisationService.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IOrganisationRepository"/>.</param>
        public OrganisationService(IOrganisationRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public Organisation CreateOrganisation(string name, string description, bool isActive, Guid userGuid)
        {
            var organisation = new Organisation
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Description = description,
                IsActive = isActive,
                CreationDate = DateTime.UtcNow,
                LastManipulation = DateTime.UtcNow
            };

            return organisation;
        }

        /// <inheritdoc/>
        public Organisation GetByName(string organisationName)
        {
            return repository.GetByName(organisationName);
        }
    }
}
