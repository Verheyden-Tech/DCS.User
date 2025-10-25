using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// Provides data access functionality for <see cref="User"/> entities, enabling CRUD operations and database
    /// interactions specific to the "dbo.VT_User" table.
    /// </summary>
    /// <remarks>This repository is designed to work with the "dbo.VT_User" table in the database, where each
    /// user is uniquely identified by a GUID. It leverages the <see cref="ISqlService"/> to perform database operations
    /// and inherits common repository functionality from <see cref="RepositoryBase{TKey, TEntity}"/>.</remarks>
    public class UserRepository : RepositoryBase<Guid, User>, IUserRepository
    {
        private readonly ISqlService sqlService;

        /// <summary>
        /// Tablename for user entities on the database.
        /// </summary>
        private static readonly new string TableName = "dbo.VT_User";

        /// <summary>
        /// PrimaryKeyColumn to identify users on the table.
        /// </summary>
        private static readonly new string PrimaryKeyColumn = "Guid";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified SQL service.
        /// </summary>
        /// <param name="sqlService">The SQL service used to interact with the database.</param>
        public UserRepository(ISqlService sqlService) : base(sqlService, TableName, PrimaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
