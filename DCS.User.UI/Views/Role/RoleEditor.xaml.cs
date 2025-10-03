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
        /// <param name="role">Instance of <see cref="Role"/>.</param>
        public RoleEditor(Role role) : base(role)
        {
            InitializeComponent();

            viewModel = new RoleViewModel(role);
            this.DataContext = viewModel;

            if (!string.IsNullOrWhiteSpace(role.Name))
            {
                this.Title = role.Name;
            }

            if(viewModel.GetAllRoleMember(role) != null && viewModel.GetAllRoleMember(role).Count >= 0)
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
            if(e.Suggestion is User user)
            {
                if(!RoleMember.Contains(user))
                {
                    RoleMember.Add(user);
                }

                AddUserSuggestBox.Text = string.Empty;
            }
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is ListViewItem item && item.DataContext is User user)
            {
                if(RoleMember.Contains(user))
                {
                    RoleMember.Remove(user);
                }
            }
        }
    }
}