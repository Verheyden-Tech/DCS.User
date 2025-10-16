using DCS.CoreLib.BaseClass;
using DCS.Data;

namespace DCS.User.Data
{
    /// <summary>
    /// Provides data access functionality for the <see cref="UserDomain"/> entity, enabling operations such as
    /// retrieval, insertion, updating, and deletion of user domain records in the database.
    /// </summary>
    /// <remarks>This repository is designed to interact with the database table <c>VT_User_Domain</c>, using
    /// <c>Guid</c> as the primary key. It leverages the provided <see cref="ISqlService"/> to perform SQL operations.
    /// The repository is intended to be used in scenarios where user domain data needs to be managed
    /// programmatically.</remarks>
    public class UserDomainRepository : RepositoryBase<Guid, UserDomain>, IUserDomainRepository
    {
        private readonly ISqlService sqlService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISqlService>();

        private static readonly new string TableName = "VT_User_Domain";
        private static readonly new string PrimaryKeyColumn = "Guid";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainRepository"/> class,  providing access to user domain
        /// data stored in the database.
        /// </summary>
        /// <param name="sqlService">The <see cref="ISqlService"/> instance used to interact with the database. This service is required to
        /// perform SQL operations.</param>
        public UserDomainRepository(ISqlService sqlService) : base(sqlService, TableName, PrimaryKeyColumn)
        {
            this.sqlService = sqlService;
        }
    }
}
