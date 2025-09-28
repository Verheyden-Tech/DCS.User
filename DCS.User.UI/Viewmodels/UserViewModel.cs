using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Implements the <see cref="ViewModelBase{TKey, TModel}"/> class for the <see cref="User"/> model.
    /// </summary>
    public class UserViewModel : ViewModelBase<Guid, User>
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();
        private readonly IOrganisationService organisationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IOrganisationService>();
        private readonly IRoleService roleService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IRoleService>();

        #region List Initializations
        /// <summary>
        /// Contains all avialable user groups from the table.
        /// </summary>
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();
        /// <summary>
        /// Contains all avialable user organisations from the table.
        /// </summary>
        private ObservableCollection<Organisation> organisations = new ObservableCollection<Organisation>();
        /// <summary>
        /// Contains all avialable user roles from the table.
        /// </summary>
        private ObservableCollection<Role> roles = new ObservableCollection<Role>();
        /// <summary>
        /// Contains all added user groups.
        /// </summary>
        private ObservableCollection<Group> userGroups = new ObservableCollection<Group>();
        /// <summary>
        /// Contains all added user organisations.
        /// </summary>
        private ObservableCollection<Organisation> userOrganisations = new ObservableCollection<Organisation>();
        /// <summary>
        /// Contains all added user roles.
        /// </summary>
        private ObservableCollection<Role> userRoles = new ObservableCollection<Role>();
        #endregion

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="UserViewModel"/>.
        /// </summary>
        public UserViewModel(User user) : base(user)
        {
            this.Model = user;

            groups = groupService.GetAll();
            organisations = organisationService.GetAll();
            roles = roleService.GetAll();
        }

        /// <summary>
        /// Saves the current instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <returns>True if the save was successful; otherwise, false.</returns>
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

        #region Lists
        /// <summary>
        /// Contains all avialable user groups from the table.
        /// </summary>
        public ObservableCollection<Group> AllGroups
        {
            get => groups;
            set
            {
                if(groups != value)
                {
                    groups = value;
                    OnPropertyChanged(nameof(AllGroups));
                }
            }
        }

        /// <summary>
        /// Contains all added user groups.
        /// </summary>
        public ObservableCollection<Group> UserGroups
        {
            get => userGroups;
            set
            {
                if (userGroups != value)
                {
                    userGroups = value;
                    OnPropertyChanged(nameof(UserGroups));
                }
            }
        }

        /// <summary>
        /// Contains all avialable user organisations from the table.
        /// </summary>
        public ObservableCollection<Organisation> AllOrganisations
        {
            get => organisations;
            set
            {
                if (organisations != value)
                {
                    organisations = value;
                    OnPropertyChanged(nameof(AllOrganisations));
                }
            }
        }

        /// <summary>
        /// Contains all added user organisations.
        /// </summary>
        public ObservableCollection<Organisation> UserOrganisations
        {
            get => userOrganisations;
            set
            {
                if (userOrganisations != value)
                {
                    userOrganisations = value;
                    OnPropertyChanged(nameof(UserOrganisations));
                }
            }
        }

        /// <summary>
        /// Contains all avialable user roles from the table.
        /// </summary>
        public ObservableCollection<Role> AllRoles
        {
            get => roles;
            set
            {
                if (roles != value)
                {
                    roles = value;
                    OnPropertyChanged(nameof(AllRoles));
                }
            }
        }

        /// <summary>
        /// Contains all added user roles.
        /// </summary>
        public ObservableCollection<Role> UserRoles
        {
            get => userRoles;
            set
            {
                if (userRoles != value)
                {
                    userRoles = value;
                    OnPropertyChanged(nameof(UserRoles));
                }
            }
        }
        #endregion

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
