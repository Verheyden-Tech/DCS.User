using DCS.DefaultTemplates;
using System.ComponentModel;

namespace DCS.User.UI
{
    public class UserLoginViewModel : ViewModelBase<User>, INotifyPropertyChanged
    {
        private IUserLoginService userLoginService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserLoginService>();
        private string selectedDB;
        private DefaultCollection<string> userNames;
        private bool keepLoggedIn;

        public UserLoginViewModel()
        {

        }

        #region Constructor
        public UserLoginViewModel(User user) : this()
        {
            this.Model = user;
        }

        public UserLoginViewModel(string selectedDB) : this()
        {
            this.SelectedDB = selectedDB;
        }
        #endregion

        public bool LoginUser(string username, string password)
        {
            if(userLoginService.LoginUser(username, password))
            {
                return true;
            };
            return false;
        }

        public DefaultCollection<string> SetItemsSource(string connectionType)
        {
            userNames = new DefaultCollection<string>();

            if(connectionType == "Home")
            {
                foreach (var user in userLoginService.LoadUsers())
                {
                    userNames.Add(user.UserName);
                }
            }

            return userNames;
        }

        public string SelectedDB
        {
            get
            {
                return selectedDB;
            }
            set
            {
                selectedDB = value;
                OnPropertyChanged(nameof(SelectedDB));
            }
        }

        public string UserName
        {
            get
            {
                return Model.UserName;
            }
            set
            {
                Model.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public bool KeepLoggedIn
        {
            get
            {
                return Model.KeepLoggedIn;
            }
            set
            {
                Model.KeepLoggedIn = value;
                OnPropertyChanged(nameof(KeepLoggedIn));
            }
        }

        public DefaultCollection<string> UserNames
        {
            get
            {
                return userNames;
            }
            set
            {
                userNames = value;
                OnPropertyChanged(nameof(UserNames));
            }
        }
    }
}
