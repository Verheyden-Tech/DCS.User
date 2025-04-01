using DCS.DefaultTemplates;

namespace DCS.User.UI
{
    public class RegistrateUserViewModel : ViewModelBase<Guid, User>
    {
        private IUserRepository repository = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserRepository>();

        public RegistrateUserViewModel() 
        {

        }

        public RegistrateUserViewModel(User user)
        {
            this.Model = user;
        }

        public bool RegistrateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (repository.New(user))
            {
                this.Model = user;
                return true;
            }

            return false;
        }

        #region Public Props
        public string UserName
        {
            get => this.Model.UserName;
            set
            {
                this.Model.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string PassWord
        {
            get => this.Model.PassWord;
            set
            {
                this.Model.PassWord = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }

        public bool IsAdmin
        {
            get => this.Model.IsAdmin;
            set
            {
                this.Model.IsAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsLocalUser
        {
            get => this.Model.IsLocalUser;
            set
            {
                this.Model.IsLocalUser = value;
                OnPropertyChanged(nameof(IsLocalUser));
            }
        }
        #endregion
    }
}
