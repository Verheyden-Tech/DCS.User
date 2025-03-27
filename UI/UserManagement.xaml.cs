using DCS.DefaultTemplates;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : UserControl
    {
        private DefaultCollection<User> users;
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private DataGridCellInfo selectedRow;
        private UserManagementViewModel viewModel;

        public UserManagement()
        {
            InitializeComponent();

            viewModel = new UserManagementViewModel();

            SetContextMenu();
        }

        public UserManagement(User user)
        {
            InitializeComponent();

            viewModel = new UserManagementViewModel(user);

            users = new DefaultCollection<User>();
            users = LoadUserData();
            DataGrid.ItemsSource = users;

            SetContextMenu();
        }

        public DefaultCollection<User> LoadUserData()
        {
            return users = userService.GetAll();
        }

        public DataGrid DataGrid
        {
            get
            {
                return this.UserDataGrid;
            }
            set
            {
                this.UserDataGrid = value;
            }
        }

        public DataGridCellInfo SelectedRow
        {
            get
            {
                return selectedRow;
            }
            set
            {
                selectedRow = value;
            }
        }

        private void SetContextMenu()
        {
            MenuItem newUser = new MenuItem()
            {
                Header = "Neuer User",
                Icon = "/Images/usermanagement_add_user_16x.png"
            };
            newUser.Click += NewUser_Click;

            UserManagementContextMenu.Items.Add(newUser);

            MenuItem editUser = new MenuItem()
            {
                Header = "Nutzer bearbeiten",
                Icon = "/Images/usermanagement_edit_user_16x.png"
            };
            editUser.Click += EditUser_Click;

            UserManagementContextMenu.Items.Add(editUser);

            MenuItem deleteUser = new MenuItem()
            {
                Header = "User löschen",
                Icon = "/Images/usermanagement_remove_user_16x.png"
            };
            deleteUser.Click += DeleteUser_Click;

            UserManagementContextMenu.Items.Add(deleteUser);
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Möchten Sie den Nutzer wirklich löschen?", "Nutzer löschen", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
            {
                if(UserDataGrid.SelectedCells.Remove(SelectedRow))
                {
                    DataGrid.Items.Refresh();
                }
            }
            else
            {
                return;
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new RegistrateUser();
            if(win.ShowDialog() == true)
            {
                UserDataGrid.Items.Refresh();
            };
        }

        private void UserDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedRow = (sender as DataGrid).SelectedCells as DefaultCollection<User>;
            
        }
    }
}
