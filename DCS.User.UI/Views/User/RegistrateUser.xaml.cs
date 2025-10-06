using DCS.CoreLib.View;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RegistrateUser.xaml
    /// </summary>
    public partial class RegistrateUser : DefaultMainWindow
    {
        private RegistrateUserViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrateUser"/> class with the specified domain name.
        /// </summary>
        /// <remarks>This constructor sets up the data context for the user registration view by creating
        /// a new instance of <see cref="RegistrateUserViewModel"/> and associating it with the provided domain
        /// name.</remarks>
        /// <param name="domainName">The domain name to associate with the user registration process. This value is used to initialize the <see
        /// cref="RegistrateUserViewModel"/> and set its domain context.</param>
        public RegistrateUser(string domainName)
        {
            InitializeComponent();

            this.DomainName = domainName;

            var obj = new User();
            viewModel = new RegistrateUserViewModel(obj);
            viewModel.Domain = domainName;
            this.DataContext = viewModel;
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PassWordBox.Password == PassWordRepeatBox.Password)
            {
                viewModel.PassWord = PassWordBox.Password;

                if (viewModel.RegistrateUser())
                {
                    this.NewRegistratedUser = viewModel.Model;
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

        /// <summary>
        /// Gets or sets the domain name.
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the new registrated user.
        /// </summary>
        public User NewRegistratedUser { get; set; }

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
