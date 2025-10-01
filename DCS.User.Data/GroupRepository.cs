using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// Represents the repository for managing <see cref="Group"/> entities in the database.
    /// </summary>
    public class GroupRepository : RepositoryBase<Guid, Group>, IGroupRepository
    {
        private readonly ISqlService sqlService;

        private static string tableName => "dbo.VT_User_Group";

        private static string primaryKeyColumn => "Guid";

        /// <summary>
        /// Default constructor for GroupRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public GroupRepository(ISqlService sqlService) : base(sqlService, tableName, primaryKeyColumn)
        {
            this.sqlService = sqlService;
        }

        /// <inheritdoc/>
        public Group GetByName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentNullException(nameof(groupName));

            string sql = $"SELECT * FROM {tableName} WHERE Name = @groupname";

            return sqlService.SQLQuery<Group>(sql, new { groupname = groupName });
        }
    }
}
