using DCS.CoreLib.View;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Represents the editor window for <see cref="User"/> instances.
    /// </summary>
    public partial class UserEditor : DefaultEditorWindow
    {
        private UserViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEditor"/> class.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="UserEditor"/> component and sets up the
        /// necessary resources.</remarks>
        public UserEditor() : base()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new UserViewModel(obj);
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEditor"/> class, setting up the user interface and data
        /// context for editing the specified user.
        /// </summary>
        /// <remarks>The constructor initializes the data context with a <see cref="UserViewModel"/> based
        /// on the provided user. It also configures the visibility and state of UI elements based on the properties of
        /// the <paramref name="user"/>: <list type="bullet"> <item> <description>If the user's <see
        /// cref="User.UserName"/> is not null or whitespace, the window title is set to the user's name.</description>
        /// </item> <item> <description>If the user's <see cref="User.PassWord"/> is not null or empty, the password
        /// change button is shown, and the password input box is hidden.</description> </item> <item> <description>If
        /// the user's <see cref="User.ProfilePicturePath"/> is null or empty, the profile picture button is enabled and
        /// visible, and the profile picture display is hidden.</description> </item> </list></remarks>
        /// <param name="user">The <see cref="User"/> object to be edited. This parameter must not be null.</param>
        public UserEditor(User user) : base(user)
        {
            viewModel = new UserViewModel(user);
            DataContext = viewModel;

            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                Title = user.UserName;
            }

            if (!string.IsNullOrEmpty(user.PassWord))
            {
                ChangeUserPasswordButton.Visibility = System.Windows.Visibility.Visible;
                UserPasswordBox.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (user.ProfilePicturePath == string.Empty || user.ProfilePicturePath == null)
            {
                if(AddUserProfilePictureButton != null && UserProfilePicture != null)
                {
                    AddUserProfilePictureButton.Visibility = Visibility.Visible;
                    UserProfilePicture.Visibility = Visibility.Collapsed;
                }
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
                if (win.ShowDialog() == true)
                {
                    Log.LogManager.Singleton.Info($"Password changed for user {Current.UserName}.", "UserEditor");
                    MessageBox.Show("Das Passwort wurde erfolgreich geändert.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AddUserProfilePictureButton_Click(object sender, RoutedEventArgs e)
        {
            if (Current.SetUserProfilePicture())
            {
                AddUserProfilePictureButton.Visibility = Visibility.Collapsed;
                UserProfilePicture.Visibility = Visibility.Visible;
            }
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext != null)
            {
                if (button.DataContext is Group group && Current.UserGroups.Contains(group))
                {
                    Current.RemoveUserFromGroup(group);
                }
                else if (button.DataContext is Organisation organisation && Current.UserOrganisations.Contains(organisation))
                {
                    Current.RemoveUserFromOrganisation(organisation);
                }
                else if (button.DataContext is Role role && Current.UserRoles.Contains(role))
                {
                    Current.RemoveUserFromRole(role);
                }
            }
        }

        private void GroupListBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GroupListBox.Items.Refresh();
        }

        private void OrganisationListBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OrganisationListBox.Items.Refresh();
        }

        private void OrganisationAutoCompleteBox_QuerySubmitted(object sender, Telerik.Windows.Controls.AutoSuggestBox.QuerySubmittedEventArgs e)
        {
            if (e.Suggestion is Organisation organisation && organisation != null && sender is RadAutoSuggestBox box)
            {
                if (!Current.UserOrganisations.Contains(organisation))
                {
                    if (Current.AddUserToOrganisation(organisation))
                        box.Text = string.Empty;
                }
            }
        }

        private void GroupAutoCompleteBox_QuerySubmitted(object sender, Telerik.Windows.Controls.AutoSuggestBox.QuerySubmittedEventArgs e)
        {
            if (e.Suggestion is Group group && group != null && sender is RadAutoSuggestBox box)
            {
                if (!Current.UserGroups.Contains(group))
                {
                    if (Current.AddUserToGroup(group))
                        box.Text = string.Empty;
                }
            }
        }
    }
}
