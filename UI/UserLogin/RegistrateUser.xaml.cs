using System.Windows;
using DCS.DefaultViewControls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RegistrateUser.xaml
    /// </summary>
    public partial class RegistrateUser : DefaultMainWindow
    {
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private RegistrateUserViewModel viewModel;

        public RegistrateUser()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new RegistrateUserViewModel(obj);
            this.DataContext = viewModel;
        }

        public RegistrateUser(string userName) : this()
        {
            var obj = new User() { UserName = userName};
            viewModel = new RegistrateUserViewModel(obj);
            this.DataContext = viewModel;
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            if(PassWordBox.Password == PassWordRepeatBox.Password)
            {
                var user = userService.CreateUser(UserNameTextBox.Text, PassWordBox.Password, IsAdminCheckBox.IsChecked.HasValue, KeepLoggedInCheckBox.IsChecked.HasValue);

                if(viewModel.RegistrateUser(user) == true)
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

        public string ConnectionString { get; set; }

        public User NewRegistratedUser { get; set; }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Möchten Sie den Vorgang wirklich abbrechen?", "Warnung", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                this.DialogResult = false;
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
