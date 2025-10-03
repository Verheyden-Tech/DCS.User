using DCS.CoreLib.View;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for GroupEditor.xaml
    /// </summary>
    public partial class GroupEditor : DefaultEditorWindow
    {
        private GroupViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEditor"/> class with the specified group.
        /// </summary>
        /// <remarks>This constructor sets up the data context for the editor using a <see
        /// cref="GroupViewModel"/> initialized with the provided <paramref name="group"/>. Ensure that the <paramref
        /// name="group"/> parameter is not <see langword="null"/> before calling this constructor.</remarks>
        /// <param name="group">The <see cref="Group"/> instance to be edited. Cannot be <see langword="null"/>.</param>
        public GroupEditor(Group group) : base(group)
        {
            InitializeComponent();

            this.viewModel = new GroupViewModel(group);
            this.DataContext = viewModel;

            if(!string.IsNullOrWhiteSpace(group.Name))
            {
                this.Title = group.Name;
            }
        }

        /// <summary>
        /// Returns the current instance of the <see cref="GroupViewModel"/> class as DataContext.
        /// </summary>
        public GroupViewModel Current
        {
            get
            {
                return DataContext as GroupViewModel;
            }
        }
    }
}
