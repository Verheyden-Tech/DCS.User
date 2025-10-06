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
        /// Initializes a new instance of the <see cref="RegistrateUserViewModel"/> class with the specified user.
        /// </summary>
        /// <param name="user">The user model to associate with this view model. Cannot be <see langword="null"/>.</param>
        public RegistrateUserViewModel(User user)
        {
            this.Model = user;
        }

        /// <summary>
        /// Registers a user in the system, either as an Active Directory (AD) user or a standard user.
        /// </summary>
        /// <remarks>This method determines the type of user based on the <see cref="IsADUser"/> property.
        /// If the user is an AD user, it creates and registers the user using Active Directory-specific logic. 
        /// Otherwise, it creates a standard user and registers them in the system.</remarks>
        /// <returns><see langword="true"/> if the user was successfully registered; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the user creation process for a standard user results in a null user object.</exception>
        public bool RegistrateUser()
        {
            if (this.IsADUser)
            {
                var adUser = new ADUser
                {
                    Guid = Guid.NewGuid(),
                    DomainName = this.UserName,
                    IsActive = true,
                    CompanyGuid = Guid.Empty
                };

                if (service.RegisterADUser(adUser))
                    return true;

                return false;
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(this.Domain))
                        return false;

                    var user = service.CreateUser(this.UserName, this.PassWord, this.IsAdmin, this.KeepLoggedIn, this.Domain);

                    if (user == null)
                        throw new ArgumentNullException(nameof(user));

                    if (service.New(user))
                    {
                        this.Model = user;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogManager.Singleton.Error($"Error while creating user: {ex.Message}.", $"{ex.Source}");
                    return false;
                }
            }
            return true;
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
            get;
            set;
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
