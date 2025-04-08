using DCS.CoreLib.View;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the editor window for <see cref="User"/> instances.
    /// </summary>
    public partial class UserEditor : DefaultEditorWindow
    {
        private UserViewModel viewModel;

        /// <summary>
        /// Default constructor initialize a new <see cref="UserEditor"/> window.
        /// </summary>
        public UserEditor(User user) : base(user)
        {
            InitializeComponent();

            this.viewModel = new UserViewModel(user);
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Returns the current instance of the <see cref="UserViewModel"/> class as DataContext.
        /// </summary>
        public UserViewModel Current
        {
            get
            {
                return DataContext as UserViewModel;
            }
        }
    }
}
