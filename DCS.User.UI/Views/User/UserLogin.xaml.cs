using DCS.CoreLib.View;
using DCS.Localization;
using DCS.Resource;
using System.Globalization;
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
        private readonly ILocalizationService localizationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILocalizationService>();
        private readonly IIconService iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();
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

            UserNameLoginTextBox.Text = localizationService.Translate("InputUsername");
            DomainComboBox.EmptyText = localizationService.Translate("SelectDomain");

            if (Current.Domains != null && Current.Domains.Count > 0)
                DomainComboBox.SelectedItem = Current.Domains.FirstOrDefault();

            var cultures = new List<CultureInfo>(CurrentSessionService.Instance.GetAvailableCultures());
            CurrentSessionService.Instance.SetCurrentUserCulture(cultures.Where(c => c.Name == "de-DE").First());
            CurrentLanguageTextBlock.Text = CurrentSessionService.Instance.CurrentUserCulture.DisplayName;
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
            if (string.IsNullOrWhiteSpace(PassWordLoginBox.Password))
            {
                MessageBox.Show(localizationService.Translate("PasswordCannotBeEmpty"), localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(!string.IsNullOrWhiteSpace(CurrentLanguageTextBlock.Text))
            {
                if(CurrentSessionService.Instance.CurrentUserCulture.DisplayName != CurrentLanguageTextBlock.Text)
                {
                    var culture = CurrentSessionService.Instance.GetAvailableCultures().Where(c => c.DisplayName == CurrentLanguageTextBlock.Text).First();
                    CurrentSessionService.Instance.SetCurrentUserCulture(culture);
                    if(Current.Language != CurrentSessionService.Instance.CurrentUserCulture.DisplayName)
                    {
                        Current.Language = CurrentSessionService.Instance.CurrentUserCulture.DisplayName;
                        if (Current.UpdateUser())
                            Log.LogManager.Singleton.Info($"User language preference updated to {Current.Language} for user account.", $"{LoginButton_Click}");
                    }
                }
            }

            if (Current.LoginUser(PassWordLoginBox.Password))
            {
                if (KeepLoggedInCheckBox.IsChecked == true)
                {
                    Current.KeepLoggedIn = true;
                    if (Current.UpdateUser())
                        Log.LogManager.Singleton.Warning($"User chose to keep logged in for user account. Keep logged in flag succesful setted.", $"{LoginButton_Click}");
                }

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(localizationService.Translate("ErrorLogin"), localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                Log.LogManager.Singleton.Warning($"Failed login attempt for user account", $"{LoginButton_Click}");
                return;
            }
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new RegistrateUser();
            if (!string.IsNullOrWhiteSpace(CurrentSessionService.Instance.CurrentUserDomain))
            {
                if (win.ShowDialog() == true)
                {
                    if (KeepLoggedInCheckBox.IsChecked == true)
                    {
                        Current.KeepLoggedIn = true;
                        if (Current.UpdateUser())
                            Log.LogManager.Singleton.Warning($"New crated user chose to keep logged in for user account. Keep logged in flag succesful set.", $"{RegistrateButton_Click}");
                    }

                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(localizationService.Translate("SelectDomainBeforeRegister"), localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (MessageBox.Show(localizationService.Translate("CloseMessage"), localizationService.Translate("Exit"), MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
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
                if (comboBox.SelectedValue != null)
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
            var domain = Current.Domains.FirstOrDefault(d => d.DomainName == DomainComboBox.Text);

            if (domain != null && domain.DomainName != CurrentSessionService.Instance.CurrentUserDomain)
            {
                CurrentSessionService.Instance.UnsetCurrentUserDomain();
                CurrentSessionService.Instance.SetCurrentUserDomain(domain.DomainName);
            }
        }

        private void LanguageDropDownButton_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is RadComboBox box && box.SelectedItem is CultureInfo culture)
            {
                if(!localizationService.SetLanguage(culture.Name))
                {
                    Log.LogManager.Singleton.Info($"Failed to changed application language to {culture.DisplayName}", "UserLogin");
                    MessageBox.Show(localizationService.Translate("ErrorChangingLanguage") + $"{culture.DisplayName}", localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                CurrentSessionService.Instance.SetCurrentUserCulture(culture);
                LanguageFlagImage.Source = iconService.GetLanguageFlag(CurrentSessionService.Instance.CurrentUserCulture.TwoLetterISOLanguageName);
                MessageBox.Show(localizationService.Translate("LanguageChanged") + $"{culture.DisplayName}", localizationService.Translate("Success"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(localizationService.Translate("FailedChangeLanguageTry"), localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeUserLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new UserLanguageSelector();
            if (win.ShowDialog() == true)
            {
                LanguageFlagImage.Visibility = Visibility.Visible;
                LanguageFlagImage.Source = iconService.GetLanguageFlag(CurrentSessionService.Instance.CurrentUserCulture.TwoLetterISOLanguageName);
                CurrentLanguageTextBlock.Text = CurrentSessionService.Instance.CurrentUserCulture.DisplayName;
                Log.LogManager.Singleton.Info($"User changed application language to {CurrentSessionService.Instance.CurrentUserCulture.DisplayName}", $"{ChangeUserLanguageButton_Click}");
                MessageBox.Show(localizationService.Translate("LanguageChanged") + $"{CurrentSessionService.Instance.CurrentUserCulture.DisplayName}", localizationService.Translate("Success"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
