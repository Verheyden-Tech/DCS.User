using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// Represents the repository to manage <see cref="Role"/> entities in the database.
    /// </summary>
    public class RoleRepository : RepositoryBase<Guid, Role>, IRoleRepository
    {
        private readonly ISqlService sqlService;
        /// <summary>
        /// Tablename for the RoleRepository.
        /// </summary>
        private static string tableName => "dbo.VT_User_Role";
        /// <summary>
        /// PrimaryKeyColumn to identify roles on the table.
        /// </summary>
        private static string primaryKeyColumn => "Guid";
        /// <summary>
        /// Default constructor for RoleRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public RoleRepository(ISqlService sqlService) : base(sqlService, tableName, primaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
