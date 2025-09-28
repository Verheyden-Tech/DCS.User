using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;

namespace DCS.User.Viewmodels
{
    /// <summary>
    /// Represents the ViewModel for the <see cref="Role"/> model. Implements the <see cref="ViewModelBase{TKey, TModel}"/> class.
    /// </summary>
    public class RoleViewModel : ViewModelBase<Guid, Role>
    {
        private readonly IRoleService roleService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IRoleService>();

        /// <summary>
        /// Default constructor to initialize a new instance of <see cref="RoleViewModel"/>.
        /// </summary>
        /// <param name="role">Instance of <see cref="Role"/>.</param>
        public RoleViewModel(Role role) : base(role)
        {
            this.Model = role;
        }

        /// <summary>
        /// Saves the current instance of the <see cref="RoleViewModel"/> class.
        /// </summary>
        /// <returns>True if the save was succesful; otherwise, false.</returns>
        public bool Save()
        {
            if (Model != null)
            {
                Model.LastManipulation = DateTime.Now;
                if (roleService.Update(Model))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Gets all available active roles from the table.
        /// </summary>
        /// <returns>All avialable active roles from the table.</returns>
        public ObservableCollection<Role> GetAllActiveRoles()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();

            var allRoles = roleService.GetAll();

            if (allRoles != null && allRoles.Count > 0)
            {
                foreach (var role in allRoles)
                {
                    if (role.IsActive)
                    {
                        roles.Add(role);
                    }
                }
            }

            return roles;
        }

        #region Properties
        /// <summary>
        /// Role unique identifier.
        /// </summary>
        public Guid Guid
        {
            get { return Model.Guid; }
            set
            {
                if (Model.Guid != value)
                {
                    Model.Guid = value;
                    OnPropertyChanged(nameof(Guid));
                }
            }
        }

        /// <summary>
        /// Role ident.
        /// </summary>
        public int Ident
        {
            get { return Model.Ident; }
            set
            {
                if (Model.Ident != value)
                {
                    Model.Ident = value;
                    OnPropertyChanged(nameof(Ident));
                }
            }
        }

        /// <summary>
        /// Role name.
        /// </summary>
        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Role description.
        /// </summary>
        public string Description
        {
            get { return Model.Description; }
            set
            {
                if (Model.Description != value)
                {
                    Model.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        /// <summary>
        /// Role is active flag.
        /// </summary>
        public bool IsActive
        {
            get { return Model.IsActive; }
            set
            {
                if (Model.IsActive != value)
                {
                    Model.IsActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        /// <summary>
        /// Role creator user unique identifier.
        /// </summary>
        public Guid UserGuid
        {
            get { return Model.UserGuid; }
            set
            {
                if (Model.UserGuid != value)
                {
                    Model.UserGuid = value;
                    OnPropertyChanged(nameof(UserGuid));
                }
            }
        }

        /// <summary>
        /// Role creation date.
        /// </summary>
        public DateTime? CreationDate
        {
            get { return Model.CreationDate; }
            set
            {
                if (Model.CreationDate != value)
                {
                    Model.CreationDate = value;
                    OnPropertyChanged(nameof(CreationDate));
                }
            }
        }

        /// <summary>
        /// Role properties last time changed date.
        /// </summary>
        public DateTime? LastManipulation
        {
            get { return Model.LastManipulation; }
            set
            {
                if (Model.LastManipulation != value)
                {
                    Model.LastManipulation = value;
                    OnPropertyChanged(nameof(LastManipulation));
                }
            }
        }
        #endregion
    }
}
