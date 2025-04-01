using DCS.DefaultTemplates;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DCS.User.UI
{
    /// <summary>
    /// ViewModel for the <see cref="UserLogin"/>.
    /// </summary>
    public class UserLoginViewModel : ViewModelBase<Guid, User>, INotifyPropertyChanged
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private string selectedDB;
        private DefaultCollection<string> userNames;

        /// <summary>
        /// Default constuctor initializes a new instance of <see cref="UserLoginViewModel"/>
        /// </summary>
        public UserLoginViewModel(User user)
        {
            this.Model = user;
        }

        /// <summary>
        /// Checks if given user credentials equals to the data on the table.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">User raw password.</param>
        /// <returns>Wether the login was succesfull.</returns>
        public bool LoginUser(string username, string password)
        {
            if(userService.LoginUser(username, password))
            {
                return true;
            };
            return false;
        }

        /// <summary>
        /// Sets the items source for the database selector in the <see cref="UserLogin"/>
        /// </summary>
        /// <param name="connectionType">Network enviroment type.</param>
        /// <returns></returns>
        public ObservableCollection<string> SetItemsSource(string connectionType)
        {
            userNames = new DefaultCollection<string>();

            if(connectionType == "Home")
            {
                foreach (var user in userService.GetAll())
                {
                    userNames.Add(user.UserName);
                }
            }

            return userNames;
        }

        /// <summary>
        /// Gets or sets the selected database as string.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
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

        /// <summary>
        /// Indicates if a user shall be keep logged in.
        /// </summary>
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

        /// <summary>
        /// Contains avialable user account names.
        /// </summary>
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
