using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// Represents the repository for managing <see cref="Group"/> entities in the database.
    /// </summary>
    public class GroupRepository : RepositoryBase<Guid, Group>, IGroupRepository
    {
        private readonly ISqlService sqlService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISqlService>();

        private static readonly new string TableName = "dbo.VT_User_Group";

        private static readonly new string PrimaryKeyColumn = "Guid";

        /// <summary>
        /// Default constructor for GroupRepository.
        /// </summary>
        /// <param name="sqlService">Sql service.</param>
        public GroupRepository(ISqlService sqlService) : base(sqlService, TableName, PrimaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
