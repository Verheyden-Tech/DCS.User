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
        private readonly IDataBaseService dataBaseManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IDataBaseService>();
        private string connectionString;
        private ObservableCollection<string> dbServers;
        private UserLoginViewModel viewModel;

        /// <summary>
        /// Default constructor to initializes a new instance of <see cref="UserLogin"/>.
        /// </summary>
        public UserLogin()
        {
            InitializeComponent();

            SetImages();

            ConnectionString = "Home";

            SetUpDataBases();

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
                var win = new RegistrateUser(UserNameLoginComboBox.Text);
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
                var win = new RegistrateUser();
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

        private void TestDBConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            if(dataBaseManager.TestConnection(ConnectionString))
            {
                MessageBox.Show("DB-Verbindung erfolgreich.", "Erfolg!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Verbindungsfehler.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ServerComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.SelectedDB = string.Empty;

            this.SelectedDB = (sender as RadComboBox).SelectedItem as string;
            ConnectionString = GetConnectionString(SelectedDB);

            if(SelectedDB != "LocalUser")
            {
                TestDBConnectionButton.IsEnabled = true;
                TestDBConnectionButton.Visibility = Visibility.Visible;
            }

            var userAccounts = viewModel.SetItemsSource(SelectedDB);

            if(userAccounts.Count > 0)
            {
                UserNameLoginTextBox.Visibility = Visibility.Hidden;
                UserNameLoginComboBox.Visibility = Visibility.Visible;
                UserNameLoginComboBox.ItemsSource = userAccounts;
            }
        }

        private void SetUpDataBases()
        {
            dbServers = new ObservableCollection<string>();

            var server1 = "Home";
            dbServers.Add(server1);

            var server2 = "Office";
            dbServers.Add(server2);

            ServerComboBox.ItemsSource = dbServers;
            ServerComboBox.SelectionChanged += ServerComboBox_SelectionChanged;
        }

        private static string GetConnectionString(string dbName)
        {
            return dbName switch
            {
                "Home" => "Data Source=PCHOME;Initial Catalog = IT_Verheyden; Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=True;Application Intent = ReadWrite; Multi Subnet Failover=False",
                "Office" => "Data Source=TC-P360-01/DCS_DB;Initial Catalog = DCS; Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=True;Application Intent = ReadWrite; Multi Subnet Failover=False",
                "Local" => "LocalUser",
                _ => string.Empty
            };
        }

        public bool IsConnectionSelected {  get; set; }

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
                if (!string.IsNullOrEmpty(SelectedDB))
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
        public string SelectedDB { get; set; }

        /// <summary>
        /// Represents the connection string for the selected database.
        /// </summary>
        public string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }
    }
}
