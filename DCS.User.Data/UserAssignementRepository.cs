using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User
{
    /// <summary>
    /// Represents the repository for managing user assignments on the table.
    /// </summary>
    public class UserAssignementRepository : RepositoryBase<Guid, UserAssignement>, IUserAssignementRepository
    {
        private readonly ISqlService sqlService;

        private static readonly new string TableName = "dbo.VT_User_Assignement";
        private static readonly new string PrimaryKeyColumn = "Guid";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignementRepository"/> class,  providing access to user
        /// assignment data on the table.
        /// </summary>
        /// <param name="sqlService">The SQL service used to interact with the database. This parameter cannot be null.</param>
        public UserAssignementRepository(ISqlService sqlService) : base(sqlService, TableName, PrimaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
