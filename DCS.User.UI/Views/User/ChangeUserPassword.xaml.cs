using DCS.CoreLib.View;
using DCS.Resource;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for ChangeUserPassword.xaml
    /// </summary>
    public partial class ChangeUserPassword : DefaultEditorWindow
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private readonly IIconService iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();
        private UserViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserPassword"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the necessary components for the <see
        /// cref="ChangeUserPassword"/> class. Ensure that the required initialization logic is completed before using
        /// this instance.</remarks>
        public ChangeUserPassword() : base()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new UserViewModel(obj);
            DataContext = viewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserPassword"/> class with the specified user.
        /// </summary>
        /// <remarks>This constructor initializes the data context with a <see cref="UserViewModel"/>
        /// instance created for the specified user.</remarks>
        /// <param name="user">The user whose password is to be changed. This parameter cannot be <see langword="null"/>.</param>
        public ChangeUserPassword(User user) : this()
        {
            viewModel = new UserViewModel(user);
            DataContext = viewModel;

            Title = $"Passwort ändern für {user.UserName}";
        }

        /// <summary>
        /// Gets the current user view model instance as data context.
        /// </summary>
        public UserViewModel Current
        {
            get
            {
                return DataContext as UserViewModel;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Möchten Sie beenden ohne das Passwort zu speichern für {Current.UserName}?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Log.LogManager.Singleton.Info($"Password change cancelled for {Current.UserName}.", "ChangeUserPassword");
                this.Close();
            }

            return;
        }

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = CryptographyHelper.HashSHA256(CurrentUserPasswordBox.Password);
            if (!Equals(oldPassword, Current.PassWord))
            {
                MessageBox.Show("Das eingegebene aktuelle Passwort ist nicht korrekt!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.LogManager.Singleton.Warning($"The entered current password doesn´t match the stored password for {Current.UserName}.", "ChangeUserPassword");
                OldPasswordValidation.Visibility = Visibility.Visible;
                OldPasswordValidation.Content = iconService.GetValidationGlyph(false);
                CurrentUserPasswordBox.Focus();
                return;
            }
            else
            {
                OldPasswordValidation.Visibility = Visibility.Visible;
                OldPasswordValidation.Content = iconService.GetValidationGlyph(true);
            }

            if (Equals(UserPassword.Password, UserPasswordRepeat.Password))
            {

                if (MessageBox.Show($"Möchten Sie das neue Passwort speichern für {Current.UserName}?", "Speichern?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string newHashedPassword = CryptographyHelper.HashSHA256(UserPassword.Password);
                    Current.PassWord = newHashedPassword;
                    if (userService.Update(Current.Model))
                    {
                        MessageBox.Show("Passwort erfolgreich geändert.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                        Log.LogManager.Singleton.Info($"Successfully changed password for {Current.UserName}.", "ChangeUserPassword");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Speichern des neuen Passworts.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        Log.LogManager.Singleton.Error($"Error saving new password for {Current.UserName}.", "ChangeUserPassword");
                        return;
                    }
                }

                if (MessageBox.Show($"Möchten Sie beenden ohne das Passwort zu speichern für {Current.UserName}?", "Speichern?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Die eingegebenen neuen Passwörter stimmen nicht überein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.LogManager.Singleton.Warning($"The entered new passwords don´t match for {Current.UserName}.", "ChangeUserPassword");
                NewPasswordValidation.Visibility = Visibility.Visible;
                NewPasswordValidation.Content = iconService.GetValidationGlyph(false);
                RepeatPasswordValidation.Visibility = Visibility.Visible;
                RepeatPasswordValidation.Content = iconService.GetValidationGlyph(false);
                UserPasswordRepeat.Password = string.Empty;
                UserPassword.Focus();
                return;
            }
        }

        private void CurrentUserPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CurrentUserPasswordBox.Password))
            {
                string oldPassword = CryptographyHelper.HashSHA256(CurrentUserPasswordBox.Password);
                if (!Equals(oldPassword, Current.PassWord))
                {
                    MessageBox.Show("Das eingegebene aktuelle Passwort ist nicht korrekt!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    Log.LogManager.Singleton.Warning($"The entered current password doesn´t match the stored password for {Current.UserName}.", "ChangeUserPassword");
                    OldPasswordValidation.Visibility = Visibility.Visible;
                    OldPasswordValidation.Content = iconService.GetValidationGlyph(false);
                    CurrentUserPasswordBox.Focus();
                    return;
                }
                else
                {
                    OldPasswordValidation.Visibility = Visibility.Visible;
                    OldPasswordValidation.Content = iconService.GetValidationGlyph(true);
                }
            }
        }

        private void UserPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserPassword.Password))
            {
                if (UserPassword.Password.Length < 6)
                {
                    MessageBox.Show("Das neue Passwort muss mindestens 6 Zeichen lang sein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    Log.LogManager.Singleton.Warning($"The entered new password is too short for {Current.UserName}.", "ChangeUserPassword");
                    NewPasswordValidation.Visibility = Visibility.Visible;
                    NewPasswordValidation.Content = iconService.GetValidationGlyph(false);
                    UserPassword.Focus();
                    return;
                }
                else
                {
                    NewPasswordValidation.Visibility = Visibility.Visible;
                    NewPasswordValidation.Content = iconService.GetValidationGlyph(true);
                }
            }
        }

        private void UserPasswordRepeat_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserPasswordRepeat.Password))
            {
                if (!Equals(UserPasswordRepeat.Password, UserPassword.Password))
                {
                    MessageBox.Show("Die eingegebenen neuen Passwörter stimmen nicht überein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    Log.LogManager.Singleton.Warning($"The entered new passwords don´t match for {Current.UserName}.", "ChangeUserPassword");
                    NewPasswordValidation.Visibility = Visibility.Visible;
                    NewPasswordValidation.Content = iconService.GetValidationGlyph(false);
                    UserPassword.Focus();
                    return;
                }
                else
                {
                    NewPasswordValidation.Visibility = Visibility.Visible;
                    NewPasswordValidation.Content = iconService.GetValidationGlyph(true);
                }
            }
        }
    }
}
