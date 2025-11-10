using DCS.CoreLib.BaseClass;
using DCS.CoreLib.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RoleEditor.xaml
    /// </summary>
    public partial class RoleEditor : DefaultEditorWindow
    {
        private RoleViewModel viewModel;

        private ObservableCollection<User> RoleMember = new ObservableCollection<User>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleEditor"/> class.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="RoleEditor"/> component and prepares it
        /// for use.</remarks>
        public RoleEditor() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleEditor"/> class with the specified role.
        /// </summary>
        /// <remarks>This constructor sets the data context to a new instance of <see
        /// cref="RoleViewModel"/> initialized with the specified role. If the role has a non-empty name, it is used as
        /// the title of the editor. Additionally, the role's members are retrieved and assigned to the
        /// <c>RoleMember</c> property if available.</remarks>
        /// <param name="role">The role to be edited. Must not be <see langword="null"/>.</param>
        public RoleEditor(Role role) : base(role)
        {
            viewModel = new RoleViewModel(role);
            this.DataContext = viewModel;

            if (!string.IsNullOrWhiteSpace(role.Name))
            {
                this.Title = role.Name;
            }

            if (viewModel.GetAllRoleMember(role) != null && viewModel.GetAllRoleMember(role).Count >= 0)
                RoleMember = viewModel.GetAllRoleMember(role);
        }

        /// <summary>
        /// Returns the current instance of the <see cref="RoleViewModel"/> class as DataContext.
        /// </summary>
        public RoleViewModel Current
        {
            get
            {
                return DataContext as RoleViewModel;
            }
        }

        private void AddUserSuggestBox_QuerySubmitted(object sender, Telerik.Windows.Controls.AutoSuggestBox.QuerySubmittedEventArgs e)
        {
            if (e.Suggestion is User user)
            {
                if (!RoleMember.Contains(user))
                {
                    if (viewModel.AddUserToRole(user))
                        RoleMember.Add(user);
                }

                AddUserSuggestBox.Text = string.Empty;
            }
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ListViewItem item && item.DataContext is User user)
            {
                if (RoleMember.Contains(user))
                {
                    if (viewModel.RemoveUserFromRole(user))
                        RoleMember.Remove(user);
                }
            }
        }
    }
}