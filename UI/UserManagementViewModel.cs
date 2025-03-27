using DCS.DefaultTemplates;
using System.Collections.ObjectModel;
using System.Windows;

namespace DCS.User
{
    public class UserManagementViewModel : IViewModelBase<User>
    {
        private User user;
        private ObservableCollection<User> users;
        private ObservableCollection<User> adminUsers;
        private IUserRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserRepository>();
        private string connectionString;

        public UserManagementViewModel()
        {
            users = new ObservableCollection<User>();
        }

        public UserManagementViewModel(User user) : this()
        {
            this.user = user;
        }

        public UserManagementViewModel(string connectionString) : base()
        {
            this.ConnectionString = connectionString;

            repository = new UserRepository(connectionString);
        }

        #region Public Methods

        public User GetByName(string userName)
        {
            return repository.GetByName(userName);
        }

        public bool Add(User obj)
        {
            if(users != null && obj != null)
            {
                if(!users.Contains(obj))
                {
                    users.Add(obj);

                    return true;
                }

                return false;
            }

            return false;
        }

        public bool Edit(User obj)
        {
            if(users != null && obj != null)
            {
                try
                {
                    var oldItem = users.Where(u => u.Guid == obj.Guid).FirstOrDefault();

                    if (oldItem != null)
                    {
                        users.Remove(oldItem);
                        users.Add(obj);

                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Beim bearbeiten von '{obj}' ist ein fehler aufgetreten: {ex}");
                    return false;
                }
            }

            return false;
        }

        public bool Remove(User obj)
        {
            if(users != null && obj != null)
            {
                if(users.Contains(obj))
                {
                    try
                    {
                        users.Remove(obj);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Beim deaktivieren von '{obj}' ist ein fehler aufgetreten: {ex}");
                        return false;
                    }
                }

                return false;
            }

            return false;
        }
        #endregion

        #region Public Props
        public ObservableCollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
            }
        }

        public ObservableCollection<User> AdminUsers
        {
            get
            {
                return adminUsers;
            }
            set
            {
                adminUsers = value;
            }
        }

        public Guid Guid
        {
            get => Model.Guid;
            set => Model.Guid = value;
        }

        public string UserName
        {
            get => Model.UserName;
            set => Model.UserName = value;
        }

        public string PassWord
        {
            get => Model.PassWord;
            set => Model.PassWord = value;
        }

        public bool IsAdmin
        {
            get => Model.IsAdmin;
            set => Model.IsAdmin = value;
        }

        public User Model => this.user;

        public string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }
        #endregion
    }
}
