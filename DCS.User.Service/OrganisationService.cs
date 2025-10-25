using DCS.CoreLib.BaseClass;

namespace DCS.User.Service
{
    /// <summary>
    /// Represents the OrganisationService for <see cref="Organisation"/> entities.
    /// </summary>
    public class OrganisationService : ServiceBase<Guid, Organisation, IOrganisationRepository>, IOrganisationService
    {
        private readonly IOrganisationRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IOrganisationRepository>();

        /// <summary>
        /// Default constructor for OrganisationService.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IOrganisationRepository"/>.</param>
        public OrganisationService(IOrganisationRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
