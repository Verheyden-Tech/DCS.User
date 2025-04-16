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
        /// <returns>Returns true if the registtration was successful, otherwise false.</returns>
        public bool RegistrateUser()
        {
            if(this.IsADUser)
            {
                if(service.RegisterADUser(this.UserName, this.PassWord))
                    return true;
            }

            var user = service.CreateUser(this.UserName, this.PassWord, this.IsAdmin, this.KeepLoggedIn, this.Domain);

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
        /// Gets or sets the domain name of the user.
        /// </summary>
        public string Domain
        {
            get => this.Model.Domain;
            set
            {
                this.Model.Domain = value;
                OnPropertyChanged(nameof(Domain));
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
        public bool IsADUser
        {
            get => this.Model.IsADUser;
            set
            {
                this.Model.IsADUser = value;
                OnPropertyChanged(nameof(IsADUser));
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
