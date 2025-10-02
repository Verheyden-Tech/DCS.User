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

        private static string tableName => "dbo.VT_User_Assignement";
        private static string primaryKeyColumn => "Guid";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignementRepository"/> class,  providing access to user
        /// assignment data on the table.
        /// </summary>
        /// <param name="sqlService">The SQL service used to interact with the database. This parameter cannot be null.</param>
        public UserAssignementRepository(ISqlService sqlService) : base(sqlService, tableName, primaryKeyColumn)
        {
            this.sqlService = sqlService;
        }

        /// <inheritdoc/>
        public UserAssignement GetByUserAndGroup(Guid userGuid, Guid groupGuid)
        {
            var query = $"SELECT * FROM {tableName} WHERE UserGuid = @UserGuid AND GroupGuid = @GroupGuid";
            var assignement = sqlService.SQLQuery<UserAssignement>(query, new { UserGuid = userGuid, GroupGuid = groupGuid });

            return assignement;
        }

        /// <inheritdoc/>
        public UserAssignement GetByUserAndOrganisation(Guid userGuid, Guid organisationGuid)
        {
            var query = $"SELECT * FROM {tableName} WHERE UserGuid = @UserGuid AND OrganisationGuid = @OrganisationGuid";
            var assignement = sqlService.SQLQuery<UserAssignement>(query, new { UserGuid = userGuid, OrganisationGuid = organisationGuid });

            return assignement;
        }

        /// <inheritdoc/>
        public UserAssignement GetByUserAndRole(Guid userGuid, Guid roleGuid)
        {
            var query = $"SELECT * FROM {tableName} WHERE UserGuid = @UserGuid AND RoleGuid = @RoleGuid";
            var assignement = sqlService.SQLQuery<UserAssignement>(query, new { UserGuid = userGuid, RoleGuid = roleGuid });

            return assignement;
        }
    }
}
