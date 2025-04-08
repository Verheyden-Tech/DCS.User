using DCS.CoreLib.BaseClass;

namespace DCS.User.UI
{
    /// <summary>
    /// ViewModel for the <see cref="UserManagement"/> view.
    /// </summary>
    public class UserManagementViewModel : ViewModelBase<Guid, User>
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="UserManagementViewModel"/>.
        /// </summary>
        public UserManagementViewModel(User user)
        {
            this.Model = user;
        }

        /// <summary>
        /// Gets a user by its user name.
        /// </summary>
        /// <param name="userName">Given user name.</param>
        /// <returns>User instance by its username.</returns>
        public User GetByName(string userName)
        {
            return userService.GetByName(userName);
        }

        

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Guid
        {
            get => Model.Guid;
            set => Model.Guid = value;
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
        /// Gets or sets the user is admin flag.
        /// </summary>
        public bool IsAdmin
        {
            get => Model.IsAdmin;
            set => Model.IsAdmin = value;
        }
    }
}
