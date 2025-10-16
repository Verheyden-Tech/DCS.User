using DCS.CoreLib.View;
using DCS.User.Service;
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

        private UserViewModel viewModel;

        /// <summary>
        /// Default constructor to initializes a new instance of <see cref="UserLogin"/>.
        /// </summary>
        public UserLogin()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new UserViewModel(obj);
            DataContext = viewModel;

            if (Current.Domains.Count >= 0)
                ServerComboBox.Text = Current.Domains.First().DomainName;
        }

        /// <summary>
        /// Gets the current user view model as data context.
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
            if (Current.LoginUser())
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
                MessageBox.Show("Login nicht erfolgreich. Bitte überprüfen Sie ihre Anmeldedaten.", "Fehler beim Login", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.LogManager.Singleton.Warning($"Failed login attempt for user account", "UserLogin");
                return;
            }
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new RegistrateUser();
            if (!string.IsNullOrWhiteSpace(Current.Domain))
            {
                if (win.ShowDialog() == true)
                {
                    if (KeepLoggedInCheckBox.IsChecked == true)
                    {
                        userService.SetKeepLoggedIn(CurrentUserService.Instance.CurrentUser);
                    }

                    var domain = Current.Domains.FirstOrDefault(d => d.DomainName == Current.Domain);
                    if(domain != null)
                    {
                        CurrentDomainService.Instance.SetDomain(domain);
                    }
                    else
                    {
                        MessageBox.Show($"Die ausgewählte Domain '{Current.Domain}' ist nicht verfügbar.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        Log.LogManager.Singleton.Warning($"Could not find the selected domain '{Current.Domain}' in the available domains.", "UserLogin");
                        return;
                    }

                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Bitte Domain wählen vor der registrierung.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void DefaultMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(LoginButton, null);
            }
            if (e.Key == Key.Escape)
            {
                if (MessageBox.Show("Möchten Sie DCS wirklich schließen?", "DCS beenden?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    this.DialogResult = false;
                    this.Close();
                }
            }
        }

        private void ServerComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var comboBox = (sender as RadComboBox);
            if (comboBox != null)
            {
                if (!string.IsNullOrEmpty(comboBox.SelectedValue as string))
                    comboBox.ClearSelectionButtonVisibility = Visibility.Visible;
                else
                {
                    comboBox.ClearSelectionButtonVisibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        private void CreateNewDomain_Click(object sender, RoutedEventArgs e)
        {
            var win = new CreateNewUserDomain();
            if (win.ShowDialog() == true)
            {
                ServerComboBox.Text = win.DomainNameTextBox.Text;
                ServerComboBox.Items.Refresh();
            }
        }

        private void ServerComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ServerComboBox.SelectedItem is UserDomain domain && domain.DomainName != Current.Domain)
            {
                Current.Domain = domain.DomainName;
            }
        }
    }
}
