using DCS.CoreLib.View;
using DCS.OnBoarding.UI;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : DefaultAppControl
    {
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        private ObservableCollection<User> Users { get; set; }
        private UserViewModel viewModel;
        private DataGrid _userDataGrid;

        /// <summary>
        /// Default constructor for <see cref="UserManagement"/>.
        /// </summary>
        public UserManagement()
        {
            InitializeComponent();

            Users = new ObservableCollection<User>();
            Users = userService.GetAll();

            var obj = new User();
            viewModel = new UserViewModel(obj);
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

        private void GenerateUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddTestDataWindow();
            win.ShowDialog();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie den Nutzer wirklich löschen?", "Nutzer löschen", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (User user in _userDataGrid.SelectedItems)
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
            if (_userDataGrid.SelectedItems != null || _userDataGrid.SelectedItems is IList<User>)
            {
                var users = _userDataGrid.SelectedItems;

                if (users != null)
                {
                    var editor = new UserEditor();
                    editor.AddPagingObjects(users);
                    if (editor.ShowDialog() == true)
                    {
                        _userDataGrid.Items.Refresh();
                    }
                }
                else
                {
                    Log.LogManager.Singleton.Warning($"Users {users} failed to open to edit.", "UserEditor");

                    var editor = new UserEditor(Current.Model);
                    editor.AddPagingObjects(_userDataGrid.SelectedItems);
                    if (editor.ShowDialog() == true)
                    {
                        _userDataGrid.Items.Refresh();
                    }
                }
            }
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new UserEditor();
            if (win.ShowDialog() == true)
            {
                _userDataGrid.Items.Refresh();
            }
        }
    }
}
