using DCS.CoreLib.View;
using DCS.Localization;
using DCS.OnBoarding.UI;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Telerik.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : DcsInternPage
    {
        private readonly ILocalizationService localizationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILocalizationService>();
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        private ObservableCollection<User> Users { get; set; }
        private UserViewModel viewModel;
        private RadGridView _userDataGrid;

        /// <summary>
        /// Default constructor for <see cref="UserManagement"/>.
        /// </summary>
        public UserManagement()
        {
            InitializeComponent();

            Title = localizationService.Translate("UserManagement");
            DisplayName = localizationService.Translate("UserManagement");
            Name = "UserManagement";
            GroupName = "User";
            _userDataGrid = UserDataGrid;

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
            if (MessageBox.Show(localizationService.Translate("ConfirmDeleteUser"), localizationService.Translate("DeleteUser"), MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (User user in UserDataGrid.SelectedItems)
                    {
                        if (!userService.Delete(user.Guid))
                        {
                            MessageBox.Show(localizationService.Translate("ErrorOccurred") + $": {user.UserName}", localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (UserDataGrid.SelectedItems != null)
            {
                var editor = new UserEditor();
                editor.Edit(UserDataGrid.SelectedItems);
                if (editor.ShowDialog() == true)
                {
                    UserDataGrid.Items.Refresh();
                }
            }
            else
            {
                Log.LogManager.Singleton.Warning($"Users failed to open to edit.", $"{EditUser_Click}");
                return;
            }
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new UserEditor();
            if (win.ShowDialog() == true)
            {
                UserDataGrid.Items.Refresh();
            }
        }
    }
}
