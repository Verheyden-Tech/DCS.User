using DCS.CoreLib.View;
using DCS.User.Service;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : DefaultMainWindow
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();

        /// <summary>
        /// Represents the domain names for the database connection.
        /// </summary>
        public ObservableCollection<string> DomainNames { get; set; }
        private UserViewModel viewModel;

        /// <summary>
        /// Default constructor to initializes a new instance of <see cref="UserLogin"/>.
        /// </summary>
        public UserLogin()
        {
            InitializeComponent();

            DomainNames = new ObservableCollection<string>();

            GetDomainNames();

            var obj = new User();
            viewModel = new UserViewModel(obj);
            DataContext = viewModel;
        }

        /// <summary>
        /// Gets the current user view model associated as data context.
        /// </summary>
        public UserViewModel Current
        {
            get
            {
                return DataContext as UserViewModel;
            }
        }

        #region Private methods/click handler
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (userService.LoginUser(UserNameLoginTextBox.Text, PassWordLoginBox.Password, SelectedDomain))
            {
                var fullUserName = Path.Combine(SelectedDomain + "/", UserNameLoginTextBox.Text);

                LoggedInUser = userService.GetByName(fullUserName);

                if (KeepLoggedInCheckBox.IsChecked == true)
                {
                    userService.SetKeepLoggedIn(LoggedInUser);
                }

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login nicht erfolgreich. Bitte überprüfen Sie ihre Anmeldedaten.", "Fehler beim Login", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.LogManager.Singleton.Warning($"Failed login attempt for user account", "UserLogin");
                return;
            }
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
                var win = new RegistrateUser();
                if(win.ShowDialog() == true)
                {
                    if (KeepLoggedInCheckBox.IsChecked == true)
                    {
                        userService.SetKeepLoggedIn(CurrentUserService.Instance.CurrentUser);
                    }

                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bitte Domain wählen vor der registrierung.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
        }

        private void ServerComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedDomain = ServerComboBox.SelectedItem as string;

            if(SelectedDomain != null)
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void GetDomainNames()
        {
            foreach (var domain in userService.GetDomainNames())
            {
                DomainNames.Add(domain.DomainName);
            }

            ServerComboBox.ItemsSource = DomainNames;
            ServerComboBox.SelectionChanged += ServerComboBox_SelectionChanged;
        }

        private void DefaultMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoginButton_Click(LoginButton, null);
            }
            if(e.Key == Key.Escape)
            {
                if(MessageBox.Show("Möchten Sie DCS wirklich schließen?", "DCS beenden?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    this.DialogResult = false;
                    this.Close();
                }
            }
        }

        private void ServerComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var comboBox = (sender as RadComboBox);
            if(comboBox != null)
            {
                if (!string.IsNullOrEmpty(comboBox.SelectedValue as string))
                    comboBox.ClearSelectionButtonVisibility = Visibility.Visible;
                else
                {
                    comboBox.ClearSelectionButtonVisibility = Visibility.Collapsed;
                }
            }
        }

        private void UserNameLoginComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var comboBox = (sender as RadComboBox);
            if (comboBox != null)
            {
                if (!string.IsNullOrEmpty(comboBox.Text))
                    comboBox.ClearSelectionButtonVisibility = Visibility.Visible;
                else
                {
                    comboBox.ClearSelectionButtonVisibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        /// <summary>
        /// Represents the current logged in user.
        /// </summary>
        public User LoggedInUser { get; set; }

        /// <summary>
        /// Represents the current selected database.
        /// </summary>
        public string SelectedDomain { get; set; }

        private void CreateNewDomain_Click(object sender, RoutedEventArgs e)
        {
            var win = new CreateNewADUserDomain();
            if(win.ShowDialog() == true)
            {
                this.ServerComboBox.SelectedItem = win.NewDomain.DomainName;
                ServerComboBox.Items.Refresh();
            }
        }
    }
}
