using System.ComponentModel;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for ChangeUserPassword.xaml
    /// </summary>
    public partial class ChangeUserPassword : Window
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private User selectedUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserPassword"/> class with the specified user.
        /// </summary>
        /// <param name="user">The user whose password is to be changed. Cannot be <see langword="null"/>.</param>
        public ChangeUserPassword(User user)
        {
            InitializeComponent();

            this.selectedUser = user;
        }

        private void ChangeUserPassword_OnClosing(object? sender, CancelEventArgs e)
        {
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Möchten Sie beenden ohne das Passwort zu speichern für {selectedUser.UserName}?", "Speichern?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }

            return;

        }

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Equals(UserPassword.Password, UserPasswordRepeat.Password))
            {
                if (MessageBox.Show($"Möchten Sie das neue Passwort speichern für {selectedUser.UserName}?", "Speichern?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    selectedUser.PassWord = UserPassword.Password;
                    if (userService.Update(selectedUser))
                    {
                        MessageBox.Show("Passwort erfolgreich geändert.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else if (MessageBox.Show($"Möchten Sie beenden ohne das Passwort zu speichern für {selectedUser.UserName}?", "Speichern?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
