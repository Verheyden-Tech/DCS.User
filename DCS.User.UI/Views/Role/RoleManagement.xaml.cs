using CommonServiceLocator;
using DCS.CoreLib.View;
using DCS.Resource;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

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
            var glyphDelete = new RadGlyph() { Glyph = "\te10C", FontSize = 16, Foreground = Brushes.Black };
            var glyphEdit = new RadGlyph() { Glyph = "\te10B", FontSize = 16, Foreground = Brushes.Black };
            var glyphAdd = new RadGlyph() { Glyph = "\te11E", FontSize = 16, Foreground = Brushes.Black };
            var glyphGenerate = new RadGlyph() { Glyph = "\te13B", FontSize = 16, Foreground = Brushes.Black };

            MenuItem newRole = new MenuItem()
            {
                Header = "Neue Rolle",
                Icon = glyphAdd
            };
            newRole.Click += NewRole_Click;

            MenuItem editRole = new MenuItem()
            {
                Header = "Rolle bearbeiten",
                Icon = glyphEdit
            };
            editRole.Click += EditRole_Click;

            MenuItem deleteRole = new MenuItem()
            {
                Header = "Rolle löschen",
                Icon = glyphDelete
            };
            deleteRole.Click += DeleteRole_Click;

            MenuItem generateRoles = new MenuItem()
            {
                Header = "Test Rollen generieren",
                Icon = glyphGenerate
            };
            generateRoles.Click += GenerateRoles_Click;

            RoleManagementContextMenu.Items.Add(newRole);
            RoleManagementContextMenu.Items.Add(editRole);
            RoleManagementContextMenu.Items.Add(deleteRole);
        }

        private void GenerateRoles_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
