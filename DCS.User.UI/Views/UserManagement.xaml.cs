using DCS.CoreLib.View;
using DCS.OnBoarding.UI;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : DefaultAppControl
    {
        private ObservableCollection<User> Users { get; set; }
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private UserManagementViewModel viewModel;

        /// <summary>
        /// Default constructor for <see cref="UserManagement"/>.
        /// </summary>
        public UserManagement()
        {
            InitializeComponent();

            SetContextMenu();

            Users = new ObservableCollection<User>();
            Users = userService.GetAll();
            UserGridView.ItemsSource = Users;

            var obj = new User();
            viewModel = new UserManagementViewModel(obj);
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Returns the current instance of the <see cref="UserManagementViewModel"/> class as DataContext.
        /// </summary>
        public UserManagementViewModel Current
        {
            get
            {
                return DataContext as UserManagementViewModel;
            }
        }

        private void SetContextMenu()
        {
            MenuItem newUser = new MenuItem()
            {
                Header = "Neuer User",
                Icon = "Resources/Images/usermanagement_add_user_16x.png"
            };
            newUser.Click += NewUser_Click;

            UserManagementContextMenu.Items.Add(newUser);

            MenuItem editUser = new MenuItem()
            {
                Header = "Nutzer bearbeiten",
                Icon = "Resources/Images/usermanagement_edit_user_16x.png"
            };
            editUser.Click += EditUser_Click;

            UserManagementContextMenu.Items.Add(editUser);

            MenuItem deleteUser = new MenuItem()
            {
                Header = "User löschen",
                Icon = "Resources/Images/usermanagement_remove_user_16x.png"
            };
            deleteUser.Click += DeleteUser_Click;

            UserManagementContextMenu.Items.Add(deleteUser);

            MenuItem generateUser = new MenuItem()
            {
                Header = "Nutzer generieren",
                Icon = "Resources/Images/add_user_group_32x.png"
            };
            generateUser.Click += GenerateUser_Click;

            UserManagementContextMenu.Items.Add(generateUser);
        }

        private void GenerateUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddTestDataWindow();
            if(win.ShowDialog() == true)
            {
                UserGridView.Items.Refresh();
            }
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
            if (UserGridView.SelectedItems != null && UserGridView.SelectedItems is IList<User>)
            {
                var editor = new UserEditor(Current.Model);
                editor.AddPagingObjects(UserGridView.SelectedItems);
                if(editor.ShowDialog() == true)
                {
                    UserGridView.Items.Refresh();
                }
            }
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new UserEditor(Current.Model);
            if(win.ShowDialog() == true)
            {
                UserGridView.Items.Refresh();
            };
        }
    }
}
