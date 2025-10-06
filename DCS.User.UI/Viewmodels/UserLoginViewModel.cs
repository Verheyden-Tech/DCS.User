using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the view model for user login functionality, providing properties and methods to manage user
    /// authentication and related data.
    /// </summary>
    /// <remarks>This view model is designed to facilitate user login operations, including Active Directory
    /// authentication and retrieval of user-related data. It provides properties for managing user credentials,
    /// selected database, and user account names, as well as methods for logging in and retrieving user names based on
    /// connection type.</remarks>
    public class UserLoginViewModel : ViewModelBase<Guid, User>, INotifyPropertyChanged
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private string selectedDB;
        private ObservableCollection<string> userNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginViewModel"/> class with the specified user.
        /// </summary>
        /// <param name="user">The user associated with this view model. Cannot be <see langword="null"/>.</param>
        public UserLoginViewModel(User user)
        {
            this.Model = user;
        }

        /// <summary>
        /// Attempts to log in a user using the provided credentials and Active Directory domain.
        /// </summary>
        /// <param name="username">The username of the user attempting to log in. Cannot be null or empty.</param>
        /// <param name="password">The password associated with the specified username. Cannot be null or empty.</param>
        /// <param name="adDomain">The Active Directory domain to authenticate against. Cannot be null or empty.</param>
        /// <returns><see langword="true"/> if the login is successful; otherwise, <see langword="false"/>.</returns>
        public bool LoginUser(string username, string password, string adDomain)
        {
            if (userService.LoginUser(username, password, adDomain))
            {
                return true;
            };
            return false;
        }

        /// <summary>
        /// Retrieves a collection of user names based on the specified connection type.
        /// </summary>
        /// <remarks>This method initializes a new <see cref="ObservableCollection{T}"/> and populates it
        /// with user names  retrieved from the user service when the connection type is "Home."</remarks>
        /// <param name="connectionType">The type of connection to use when retrieving user names.  Must be "Home" to populate the collection;
        /// otherwise, an empty collection is returned.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> containing the user names.  The collection will be empty if the
        /// connection type is not "Home."</returns>
        public ObservableCollection<string> SetItemsSource(string connectionType)
        {
            userNames = new ObservableCollection<string>();

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
        public ObservableCollection<string> UserNames
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
