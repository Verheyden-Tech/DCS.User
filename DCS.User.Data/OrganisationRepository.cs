using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// Represents the repository for managing <see cref="Organisation"/> entities in the database.
    /// </summary>
    public class OrganisationRepository : RepositoryBase<Guid, Organisation>, IOrganisationRepository
    {
        private readonly ISqlService sqlService;
        private static string tableName => "dbo.VT_Organisation";
        private static string primaryKeyColumn => "Guid";
        /// <summary>
        /// Default constructor for OrganisationRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public OrganisationRepository(ISqlService sqlService) : base(sqlService, tableName, primaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
