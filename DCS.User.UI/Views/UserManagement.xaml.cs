using DCS.CoreLib.View;
using DCS.OnBoarding.UI;
using DCS.Resource;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : DefaultAppControl
    {
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IIconService iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();
        private ObservableCollection<User> Users { get; set; }
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
                Icon = iconService.GetImage("usermanagement_add_user_16x.png")
            };
            newUser.Click += NewUser_Click;

            MenuItem editUser = new MenuItem()
            {
                Header = "Nutzer bearbeiten",
                Icon = iconService.GetImage("usermanagement_edit_user_16x.png")
            };
            editUser.Click += EditUser_Click;

            MenuItem deleteUser = new MenuItem()
            {
                Header = "User löschen",
                Icon = iconService.GetImage("usermanagement_remove_user_16x.png")
            };
            deleteUser.Click += DeleteUser_Click;

            MenuItem generateUser = new MenuItem()
            {
                Header = "Nutzer generieren",
                Icon = iconService.GetImage("add_user_group_32x.png")
            };
            generateUser.Click += GenerateUser_Click;

            UserManagementContextMenu.Items.Add(newUser);
            UserManagementContextMenu.Items.Add(editUser);
            UserManagementContextMenu.Items.Add(deleteUser);
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
                            Log.LogManager.Singleton.Warning($"Error while deleting {user.UserName}.", "DeleteUser");
                            return;
                        }
                    }
                }
                catch (IOException ex)
                {
                    Log.LogManager.Singleton.Error($"Error while trying to delete selected users. {ex.Message}", $"{ex.Source}");
                    return;
                }
            }
            return;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserGridView.SelectedItems != null || UserGridView.SelectedItems is IList<User>)
            {
                var user = UserGridView.SelectedItems.FirstOrDefault() as User;

                if(user != null)
                {
                    var editor = new UserEditor(user);
                    editor.AddPagingObjects(UserGridView.SelectedItems);
                    if (editor.ShowDialog() == true)
                    {
                        UserGridView.Items.Refresh();
                    }
                }
                else
                {
                    Log.LogManager.Singleton.Warning($"User {user.UserName} failed to open to edit.", $"{this}");

                    var editor = new UserEditor(Current.Model);
                    editor.AddPagingObjects(UserGridView.SelectedItems);
                    if (editor.ShowDialog() == true)
                    {
                        UserGridView.Items.Refresh();
                    }
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
