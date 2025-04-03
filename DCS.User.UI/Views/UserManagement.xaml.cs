using DCS.CoreLib.BaseClass;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DCS.CoreLib.View;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : DefaultAppControl
    {
        private ObservableCollection<User> users;
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private UserManagementViewModel viewModel;

        /// <summary>
        /// Default constructor for <see cref="UserManagement"/>.
        /// </summary>
        public UserManagement()
        {
            InitializeComponent();

            SetContextMenu();

            users = new ObservableCollection<User>();
            users = userService.GetAll();
            this.UserGridView.ItemsSource = users;

            var obj = new User();
            viewModel = new UserManagementViewModel(obj);
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Load all avialable user data from the table.
        /// </summary>
        /// <returns>List auf all avialable users.</returns>
        public ObservableCollection<User> LoadUserData()
        {
            return userService.GetAll();
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
                try
                {
                    foreach (User user in UserGridView.SelectedItems)
                    {
                        if (!userService.Delete(user.Guid))
                        {
                            MessageBox.Show($"Fehler beim löschen des Benutzers {user.UserName}.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                            LogManager.LogManager.Singleton.Warning($"Error while deleting {user.UserName}.", "DeleteUser");
                            return;
                        }
                    }
                }
                catch (IOException ex)
                {
                    LogManager.LogManager.Singleton.Error($"Error while trying to delete selected users. {ex.Message}", $"{ex.Source}");
                    return;
                }
            }
            return;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new RegistrateUser();
            if(win.ShowDialog() == true)
            {
                UserGridView.Items.Refresh();
            };
        }
    }
}
