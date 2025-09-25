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
        private static string tableName => "dbo.VT_User";

        /// <summary>
        /// PrimaryKeyColumn to identify users on the table.
        /// </summary>
        private static string primaryKeyColumn => "Guid";

        /// <summary>
        /// Default constructor for UserRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public UserRepository(ISqlService sqlService) : base(sqlService, tableName, primaryKeyColumn)
        {
            this.sqlService = sqlService;
        }

        /// <inheritdoc/>
        public bool RegisterADUser(ADUser aDUser)
        {
            if(aDUser == null)
                return false;

            string sql = $"INSERT INTO dbo.VT_ADUser (Guid, DomainName, IsActive, CompanyGuid) VALUES (@Guid, @DomainName, @IsActive, @CompanyGuid)";

            return sqlService.ExecuteSQL(sql, aDUser);
        }

        /// <inheritdoc/>
        public IList<ADUser> GetDomainNames()
        {
            string sql = $"SELECT * FROM dbo.VT_ADUser";

            return sqlService.SQLQueryList<ADUser>(sql);
        }

        /// <inheritdoc/>
        public User GetByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            string sql = $"SELECT * FROM {TableName} WHERE UserName = @username";

            return sqlService.SQLQuery<User>(sql, new { username = userName});
        }

        /// <inheritdoc/>
        public User CheckForKeepLoggedIn()
        {
            string sql = $"SELECT * FROM {TableName} WHERE KeepLoggedIn = @keepLoggedIn";

            return sqlService.SQLQuery<User>(sql, new { keepLoggedIn = true });
        }

        /// <inheritdoc/>
        public bool SetKeepLoggedIn(User user)
        {
            if(user == null)
                return false;

            var sql = $"UPDATE {TableName} SET KeepLoggedIn = @keepLoggedIn WHERE {PrimaryKeyColumn} = @guid";

            return sqlService.ExecuteSQL(sql, new { keepLoggedIn = true, guid = user.Guid });
        }

        /// <inheritdoc/>
        public bool UnsetKeepLoggedIn(User user)
        {
            if (user == null)
                return false;

            var sql = $"UPDATE {TableName} SET KeepLoggedIn = @keepLoggedIn WHERE {PrimaryKeyColumn} = @guid";

            return sqlService.ExecuteSQL(sql, new { keepLoggedIn = false, guid = user.Guid });
        }
    }
}
