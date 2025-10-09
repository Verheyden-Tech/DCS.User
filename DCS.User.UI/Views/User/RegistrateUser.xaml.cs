using DCS.CoreLib.View;
using DCS.User.Service;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RegistrateUser.xaml
    /// </summary>
    public partial class RegistrateUser : DefaultMainWindow
    {
        private UserViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrateUser"/> class with the specified domain name.
        /// </summary>
        /// <remarks>This constructor sets up the data context for the user registration view by creating
        /// a new instance of <see cref="UserViewModel"/> and associating it with the provided domain
        /// name.</remarks>
        public RegistrateUser()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new UserViewModel(obj);
            this.DataContext = viewModel;
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PassWordBox.Password == PassWordRepeatBox.Password)
            {
                viewModel.PassWord = PassWordBox.Password;

                if (viewModel.RegistrateUser())
                {
                    CurrentUserService.Instance.SetUser(viewModel.Model);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Fehler beim anlegen und speichern des Accounts.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Die eingegebenen Passwörter stimmen nicht überein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie den Vorgang wirklich abbrechen?", "Warnung", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                this.DialogResult = false;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void IsADUserCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsAdminCheckBox.IsEnabled = false;
            IsAdminCheckBox.IsChecked = false;

            KeepLoggedInCheckBox.IsChecked = false;
            KeepLoggedInCheckBox.IsEnabled = false;
        }

        private void IsADUserCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsAdminCheckBox.IsEnabled = true;
            IsAdminCheckBox.IsChecked = true;

            KeepLoggedInCheckBox.IsChecked = true;
            KeepLoggedInCheckBox.IsEnabled = true;
        }
    }
}
