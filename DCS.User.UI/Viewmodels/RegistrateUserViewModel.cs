using DCS.CoreLib.BaseClass;

namespace DCS.User.UI
{
    /// <summary>
    /// ViewModel for the <see cref="RegistrateUser"/> view.
    /// </summary>
    public class RegistrateUserViewModel : ViewModelBase<Guid, User>
    {
        private IUserService service = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="RegistrateUserViewModel"/>.
        /// </summary>
        public RegistrateUserViewModel() 
        {

        }

        /// <summary>
        /// Initialize a new instance of <see cref="RegistrateUserViewModel"/> with a given user.
        /// </summary>
        /// <param name="user">Selected user.</param>
        public RegistrateUserViewModel(User user)
        {
            this.Model = user;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="user">User to registrate.</param>
        /// <returns>Returns true if the registtration was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool RegistrateUser()
        {
            var user = service.CreateUser(this.UserName, this.PassWord, this.IsAdmin, this.KeepLoggedIn);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (service.New(user))
            {
                this.Model = user;
                return true;
            }

            return false;
        }

        #region Public Props
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName
        {
            get => this.Model.UserName;
            set
            {
                this.Model.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string PassWord
        {
            get => this.Model.PassWord;
            set
            {
                this.Model.PassWord = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }

        /// <summary>
        /// Gets or sets the flag if the user has admin rights.
        /// </summary>
        public bool IsAdmin
        {
            get => this.Model.IsAdmin;
            set
            {
                this.Model.IsAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        /// <summary>
        /// Gets or sets the flag if the user is a local user.
        /// </summary>
        public bool IsLocalUser
        {
            get => this.Model.IsLocalUser;
            set
            {
                this.Model.IsLocalUser = value;
                OnPropertyChanged(nameof(IsLocalUser));
            }
        }

        /// <summary>
        /// Gets or sets the flag if the user shall be keep logged in.
        /// </summary>
        public bool KeepLoggedIn
        {
            get => this.Model.KeepLoggedIn;
            set
            {
                this.Model.KeepLoggedIn = value;
                OnPropertyChanged(nameof(KeepLoggedIn));
            }
        }
        #endregion
    }
}
