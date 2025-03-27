using System.Collections.ObjectModel;
using DCS.SQLService;
using DCS.DefaultTemplates;

namespace DCS.User
{
    public class UserRepository : IRepositoryBase<User>, IUserRepository
    {
        private readonly ISqlService sqlService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISqlService>();
        private User model;
        private ObservableCollection<User> users;

        public UserRepository()
        {
            this.users = new DefaultCollection<User>();
        }

        public UserRepository(User user) : this()
        {
            this.model = user;
            users.Add(user);
        }

        public UserRepository(string connectionString) : this() 
        {
            sqlService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ISqlService>(connectionString);
        }

        public User Get(Guid guid)
        {
            string sql = $"SELECT * FROM {TableName} WHERE {PrimaryKeyColumn} = @guid";

            return model = sqlService.SQLQuery<User>(sql, new { guid = guid});
        }

        public User GetByName(string userName)
        {
            string sql = $"SELECT * FROM {TableName} WHERE UserName = @username";

            model = sqlService.SQLQuery<User>(sql, new { username = userName});

            return model;
        }

        /// <summary>
        /// Gets the current logged in user on the table.
        /// </summary>
        /// <returns>User where IsLoggedIn = true.</returns>
        public User GetCurrentUser()
        {
            string sql = $"SELECT * FROM {TableName} WHERE IsLoggedIn = @isLoggedIn";

            model = sqlService.SQLQuery<User>(sql, new { isLoggedIn = true });

            return model;
        }

        /// <inheritdoc/>
        public User CheckForKeepLoggedIn()
        {
            string sql = $"SELECT * FROM {TableName} WHERE KeepLoggedIn = @keepLoggedIn";

            model = sqlService.SQLQuery<User>(sql, new { keepLoggedIn = true });

            return model;
        }

        /// <inheritdoc/>
        public bool SetKeepLoggedIn(User user)
        {
            var sql = $"UPDATE {TableName} SET KeepLoggedIn = @keepLoggedIn WHERE {PrimaryKeyColumn} = @guid";

            return sqlService.ExecuteSQL(sql, new { keepLoggedIn = true, guid = user.Guid });
        }

        /// <inheritdoc/>
        public bool UnsetKeepLoggedIn(User user)
        {
            var sql = $"UPDATE {TableName} SET KeepLoggedIn = @keepLoggedIn WHERE {PrimaryKeyColumn} = @guid";

            return sqlService.ExecuteSQL(sql, new { keepLoggedIn = false, guid = user.Guid });
        }

        /// <inheritdoc/>
        public DefaultCollection<User> GetAll()
        {
            users.Clear();

            string sql = $"SELECT * FROM {TableName} ORDER BY UserName";

            users = new ObservableCollection<User>();
            users = sqlService.SQLQueryList<User>(sql);

            return users as DefaultCollection<User>;
        }

        public bool New(User obj)
        {
            string sql = $"INSERT INTO {TableName} (Guid, UserName, PassWord, IsActive, KeepLoggedIn, IsAdmin, IsLoggedIn) VALUES (@guid, @userName, @passWord, @isActive, @keepLoggedIn, @isAdmin, @isLoggedIn)";

            sqlService.ExecuteSQL(sql, new { guid = obj.Guid, userName = obj.UserName, passWord = obj.PassWord, isActive = obj.IsActive, keepLoggedIn = obj.KeepLoggedIn, isAdmin = obj.IsAdmin, isLoggedIn = obj.IsLoggedIn });

            return true;
        }

        public bool Update(User obj)
        {
            string sql = $"INSERT INTO {TableName} (Guid, UserName, PassWord, IsActive, KeepLoggedIn, IsAdmin, IsLoggedIn) ON EXISTING UPDATE VALUES (@guid, @userName, @passWord, @isActive, @keepLoggedIn, @isAdmin, @isLoggedIn)";

            sqlService.ExecuteSQL(sql, new { guid = obj.Guid, userName = obj.UserName, passWord = obj.PassWord, isActive = obj.IsActive, keepLoggedIn = obj.KeepLoggedIn, isAdmin = obj.IsAdmin, isLoggedIn = obj.IsLoggedIn });

            return true;
        }

        public bool Delete(Guid userGuid)
        {
            string sql = $"UPDATE {TableName} SET IsActive = @isActive WHERE {PrimaryKeyColumn} = @guid";

            sqlService.ExecuteSQL(sql, new { guid = userGuid, isActive = false });

            return true;
        }

        public User Model => this.model;

        public string TableName => "dbo.UserData";

        public string PrimaryKeyColumn => "Guid";
    }
}
