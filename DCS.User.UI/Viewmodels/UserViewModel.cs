using DCS.CoreLib.BaseClass;

namespace DCS.User.UI
{
    /// <summary>
    /// Implements the <see cref="ViewModelBase{TKey, TModel}"/> class for the <see cref="User"/> model.
    /// </summary>
    public class UserViewModel : ViewModelBase<Guid, User>
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="UserViewModel"/>.
        /// </summary>
        public UserViewModel(User user) : base(user)
        {
            this.Model = user;
        }

        /// <summary>
        /// Saves the current instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if(Model != null)
            {
                Model.LastManipulation = DateTime.Now;

                if (userService.Update(Model))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Guid
        {
            get => Model.Guid;
            set => Model.Guid = value;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public int Ident
        {
            get => Model.Ident;
            set => Model.Ident = value;
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName
        {
            get => Model.UserName;
            set => Model.UserName = value;
        }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string PassWord
        {
            get => Model.PassWord;
            set => Model.PassWord = value;
        }

        /// <summary>
        /// Gets or sets the user domain name.
        /// </summary>
        public string Domain
        {
            get => Model.Domain;
            set => Model.Domain = value;
        }

        /// <summary>
        /// Gets or sets the user is admin flag.
        /// </summary>
        public bool IsActive
        {
            get => Model.IsActive;
            set => Model.IsActive = value;
        }

        /// <summary>
        /// Gets or sets the user keep logged in flag.
        /// </summary>
        public bool KeepLoggedIn
        {
            get => Model.KeepLoggedIn;
            set => Model.KeepLoggedIn = value;
        }

        /// <summary>
        /// Gets or sets the user has admin rights flag.
        /// </summary>
        public bool IsAdmin
        {
            get => Model.IsAdmin;
            set => Model.IsAdmin = value;
        }

        /// <summary>
        /// Gets or sets the user is logged in in current session flag.
        /// </summary>
        public bool IsLoggedIn
        {
            get => Model.IsLoggedIn;
            set => Model.IsLoggedIn = value;
        }

        /// <summary>
        /// Gets or sets the user is local user flag.
        /// </summary>
        public bool IsADUser
        {
            get => Model.IsADUser;
            set => Model.IsADUser = value;
        }

        /// <summary>
        /// Gets or sets the user creation date.
        /// </summary>
        public DateTime? CreationDate
        {
            get => Model.CreationDate;
            set => Model.CreationDate = value;
        }

        /// <summary>
        /// Gets or sets the user substitution ending date.
        /// </summary>
        public DateTime? SubstitutionEnd
        {
            get => Model.SubstitutionEnd;
            set => Model.SubstitutionEnd = value;
        }

        /// <summary>
        /// Gets or sets the user last manipulation date.
        /// </summary>
        public DateTime? LastManipulation
        {
            get => Model.LastManipulation;
            set => Model.LastManipulation = value;
        }
        #endregion
    }
}
