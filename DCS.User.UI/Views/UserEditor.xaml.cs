using DCS.CoreLib.View;
using DCS.Resource;
using DCS.User.Model;
using DCS.User.Views;
using System.Collections.ObjectModel;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the editor window for <see cref="User"/> instances.
    /// </summary>
    public partial class UserEditor : DefaultEditorWindow
    {
        private UserViewModel viewModel;
        private IIconService iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();

        /// <summary>
        /// Default constructor initialize a new <see cref="UserEditor"/> window.
        /// </summary>
        public UserEditor(User user) : base(user)
        {
            InitializeComponent();

            this.viewModel = new UserViewModel(user);
            this.DataContext = viewModel;

            if(!string.IsNullOrEmpty(user.PassWord))
            {
                this.ChangeUserPasswordButton.Visibility = System.Windows.Visibility.Visible;
                this.ChangePasswordImage.Source = iconService.GetImage("Security-Password-2-icon.png");
                this.UserPasswordBox.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Returns the current instance of the <see cref="UserViewModel"/> class as DataContext.
        /// </summary>
        public UserViewModel Current
        {
            get
            {
                return DataContext as UserViewModel;
            }
        }

        private void ChangeUserPasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Current.PassWord))
            {
                var win = new ChangeUserPassword(Current.Model);
                win.Show();
            }
        }
    }
}
