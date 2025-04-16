using DCS.CoreLib.View;
using DCS.Data;
using DCS.Resource;
using System.Collections.ObjectModel;
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
        private readonly IIconService iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();
        private string domainName;
        private ObservableCollection<string> domainNames;
        private UserLoginViewModel viewModel;

        /// <summary>
        /// Default constructor to initializes a new instance of <see cref="UserLogin"/>.
        /// </summary>
        public UserLogin()
        {
            InitializeComponent();

            SetImages();

            GetDomainNames();

            var obj = new User();
            viewModel = new UserLoginViewModel(obj);
            DataContext = viewModel;
        }

        /// <summary>
        /// Represents the current data context for a user instance.
        /// </summary>
        public UserLoginViewModel Current
        {
            get
            {
                return DataContext as UserLoginViewModel;
            }
        }

        #region Private methods/Click handler
        private void SetImages()
        {
            this.DCS_Logo_Image.Source = iconService.GetImage("DCS_Icon_Neu_86x.png");
            this.DCS_Label_Image.Source = iconService.GetImage("DCS_Label_large.png");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (userService.LoginUser(UserNameLoginComboBox.Text, PassWordLoginBox.Password))
            {
                LoggedInUser = userService.GetByName(UserNameLoginComboBox.Text);

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
                DCS.LogManager.LogManager.Singleton.Warning($"Failed login attempt for user account", "UserLogin");
                return;
            }
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(UserNameLoginComboBox.Text))
            {
                var win = new RegistrateUser(UserNameLoginComboBox.Text) { DomainName = SelectedDomain};
                if (win.ShowDialog() == true)
                {
                    this.LoggedInUser = win.NewRegistratedUser;

                    if (KeepLoggedInCheckBox.IsChecked == true)
                    {
                        userService.SetKeepLoggedIn(LoggedInUser);
                    }

                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                var win = new RegistrateUser() { DomainName = SelectedDomain };
                if(win.ShowDialog() == true)
                {
                    this.LoggedInUser = win.NewRegistratedUser;

                    if (KeepLoggedInCheckBox.IsChecked == true)
                    {
                        userService.SetKeepLoggedIn(LoggedInUser);
                    }

                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    return;
                }
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
            domainNames = new ObservableCollection<string>();

            foreach (var domain in userService.GetDomainNames())
            {
                domainNames.Add(domain.DomainName);
            }

            ServerComboBox.ItemsSource = domainNames;
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

        /// <summary>
        /// Represents the connection string for the selected database.
        /// </summary>
        public string DomainName
        {
            get => domainName;
            set => domainName= value;
        }
    }
}
