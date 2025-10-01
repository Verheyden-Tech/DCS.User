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

        private static string tableName => "dbo.VT_User_Organisation";
        private static string primaryKeyColumn => "Guid";

        /// <summary>
        /// Default constructor for OrganisationRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public OrganisationRepository(ISqlService sqlService) : base(sqlService, tableName, primaryKeyColumn)
        {
            this.sqlService = sqlService;
        }

        /// <inheritdoc/>
        public Organisation GetByName(string organisationName)
        {
            if (string.IsNullOrEmpty(organisationName))
                throw new ArgumentNullException(nameof(organisationName));

            string sql = $"SELECT * FROM {tableName} WHERE Name = @organisationName";

            return sqlService.SQLQuery<Organisation>(sql, new { organisationName });
        }
    }
}
