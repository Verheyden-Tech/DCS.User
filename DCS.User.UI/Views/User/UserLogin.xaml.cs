using DCS.CoreLib.View;
using DCS.Data.UI;
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

            if (Current.Domains != null && Current.Domains.Count > 0)
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
                    Current.KeepLoggedIn = true;
                    if(Current.UpdateUser())
                        Log.LogManager.Singleton.Warning($"User chose to keep logged in for user account. Keep logged in flag succesful setted.", "UserLogin");
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
            if (CurrentDomainService.Instance.CurrentDomain != null)
            {
                if (win.ShowDialog() == true)
                {
                    if (KeepLoggedInCheckBox.IsChecked == true)
                    {
                        Current.KeepLoggedIn = true;
                        if (Current.UpdateUser())
                            Log.LogManager.Singleton.Warning($"New crated user chose to keep logged in for user account. Keep logged in flag succesful set.", "UserRegistration");
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

        private void ServerComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var domain = Current.Domains.FirstOrDefault(d => d.DomainName == ServerComboBox.Text);

            if (domain != null && domain != CurrentDomainService.Instance.CurrentDomain)
            {
                CurrentDomainService.Instance.UnsetDomain();
                CurrentDomainService.Instance.SetDomain(domain);
            }
        }

        private void ChooseDB_Click(object sender, RoutedEventArgs e)
        {
            var dbWin = new DBManagerView();
            dbWin.Show();
        }
    }
}
