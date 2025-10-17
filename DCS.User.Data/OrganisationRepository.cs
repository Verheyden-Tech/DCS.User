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

        private static readonly new string TableName = "dbo.VT_User_Organisation";
        private static readonly new string PrimaryKeyColumn = "Guid";

        /// <summary>
        /// Default constructor for OrganisationRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public OrganisationRepository(ISqlService sqlService) : base(sqlService, TableName, PrimaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
