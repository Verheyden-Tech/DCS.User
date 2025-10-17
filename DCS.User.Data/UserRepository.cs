using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// UserRepository with basic methods inherited from <see cref="RepositoryBase{TKey, TModel}"/> to handle user data on the table.
    /// </summary>
    public class UserRepository : RepositoryBase<Guid, User>, IUserRepository
    {
        private readonly ISqlService sqlService;

        /// <summary>
        /// Tablename for the UserRepository.
        /// </summary>
        private static readonly new string TableName = "dbo.VT_User";

        /// <summary>
        /// PrimaryKeyColumn to identify users on the table.
        /// </summary>
        private static readonly new string PrimaryKeyColumn = "Guid";

        /// <summary>
        /// Default constructor for UserRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public UserRepository(ISqlService sqlService) : base(sqlService, TableName, PrimaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
