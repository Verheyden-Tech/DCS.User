using DCS.CoreLib.View;
using DCS.Resource;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommonServiceLocator;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RoleManagement.xaml
    /// </summary>
    public partial class RoleManagement : DefaultAppControl
    {
        private readonly IIconService iconService = ServiceLocator.Current.GetInstance<IIconService>();
        private readonly IRoleService roleService = ServiceLocator.Current.GetInstance<IRoleService>();

        private ObservableCollection<Role> Roles { get; set; }
        private RoleViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManagement"/> class with the specified role.
        /// </summary>
        /// <param name="role">The role to be used for initializing the role management context.</param>
        public RoleManagement()
        {
            InitializeComponent();

            SetContextMenu();

            Roles = new ObservableCollection<Role>();
            Roles = roleService.GetAll();
            RoleGridView.ItemsSource = Roles;

            var obj = new Role();
            viewModel = new RoleViewModel(obj);
            this.DataContext = viewModel;
        }

        private void SetContextMenu()
        {
            MenuItem newRole = new MenuItem()
            {
                Header = "Neue Rolle",
                Icon = iconService.GetImage("add_16x.png") // Beispiel-Icon
            };
            newRole.Click += NewRole_Click;

            MenuItem editRole = new MenuItem()
            {
                Header = "Rolle bearbeiten",
                Icon = iconService.GetImage("edit_16x.png") // Beispiel-Icon
            };
            editRole.Click += EditRole_Click;

            MenuItem deleteRole = new MenuItem()
            {
                Header = "Rolle löschen",
                Icon = iconService.GetImage("remove_16x.png") // Beispiel-Icon
            };
            deleteRole.Click += DeleteRole_Click;

            RoleManagementContextMenu.Items.Add(newRole);
            RoleManagementContextMenu.Items.Add(editRole);
            RoleManagementContextMenu.Items.Add(deleteRole);
        }

        private void NewRole_Click(object sender, RoutedEventArgs e)
        {
            var newRole = new Role();
            var editor = new RoleEditor(newRole);
            if (editor.ShowDialog() == true)
            {
                Roles.Add(newRole);
            }
        }

        private void EditRole_Click(object sender, RoutedEventArgs e)
        {
            if (RoleGridView.SelectedItem is Role selectedRole)
            {
                var editor = new RoleEditor(selectedRole);
                editor.ShowDialog();
            }
        }

        private void DeleteRole_Click(object sender, RoutedEventArgs e)
        {
            if (RoleGridView.SelectedItem is Role selectedRole)
            {
                if (MessageBox.Show($"Möchten Sie die Rolle '{selectedRole.Name}' wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    roleService.Delete(selectedRole.Guid);
                    Roles.Remove(selectedRole);
                }
            }
        }

        private void CreateNewRoleButton_Click(object sender, RoutedEventArgs e)
        {
            NewRole_Click(sender, e);
        }
    }
}
