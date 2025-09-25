using DCS.CoreLib.BaseClass;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the ViewModel for the <see cref="Group"/> model. Implements the <see cref="ViewModelBase{TKey, TModel}"/> class.
    /// </summary>
    public class GroupViewModel : ViewModelBase<Guid, Group>
    {
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();

        /// <summary>
        /// Default constructor initialize a new instance of <see cref="GroupViewModel"/>.
        /// </summary>
        /// <param name="group">Instance of <see cref="Group"/>.</param>
        public GroupViewModel(Group group) : base(group)
        {
            this.Model = group;
        }

        /// <summary>
        /// Saves the current instance of the <see cref="GroupViewModel"/> class.
        /// </summary>
        /// <returns>True if the save was successful; otherwise, false.</returns>
        public bool Save()
        {
            if (Model != null)
            {
                Model.LastManipulation = DateTime.Now;
                if (groupService.Update(Model))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        #region Properties
        /// <summary>
        /// Group unique identifier.
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
        /// Group ident.
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
        /// Group name.
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
        /// Group description.
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
        /// Group is active flag.
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
        /// Group creator user unique identifier.
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
        /// Group creation date.
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
        /// Group properties last time changed date.
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
