using System.Windows;
using DCS.CoreLib.View;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RegistrateUser.xaml
    /// </summary>
    public partial class RegistrateUser : DefaultMainWindow
    {
        private IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private RegistrateUserViewModel viewModel;

        /// <summary>
        /// Default constructor for <see cref="RegistrateUser"/>.
        /// </summary>
        public RegistrateUser()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new RegistrateUserViewModel(obj);
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="RegistrateUser"/>.
        /// </summary>
        /// <param name="userName">User name.</param>
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
                if(viewModel.RegistrateUser() == true)
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
        /// Gets or sets the connection string for the database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the new registrated user.
        /// </summary>
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
