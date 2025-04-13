using DCS.CoreLib.View;
using DCS.User.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DCS.User.Views
{
    /// <summary>
    /// Interaction logic for ChangeUserPassword.xaml
    /// </summary>
    public partial class ChangeUserPassword : DefaultEditorWindow
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private UserViewModel viewModel;
        private User selectedUser;

        /// <summary>
        /// Default constructor initialize a new <see cref="ChangeUserPassword"/> window.
        /// </summary>
        /// <param name="user">Selected user.</param>
        public ChangeUserPassword(User user) : base(user)
        {
            InitializeComponent();
            this.selectedUser = user;

            viewModel = new UserViewModel(user);
            this.DataContext = viewModel;

            this.Closing += ChangeUserPassword_OnClosing;
        }

        private void ChangeUserPassword_OnClosing(object? sender, CancelEventArgs e)
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
                else if (MessageBox.Show($"Möchten Sie das Passwort speichern für {selectedUser.UserName}?", "Speichern?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    this.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
