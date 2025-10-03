using DCS.CoreLib.View;
using DCS.Resource;
using DCS.User.Views;
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
        private IIconService iconService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IIconService>();

        /// <summary>
        /// Default constructor initialize a new <see cref="UserEditor"/> window.
        /// </summary>
        public UserEditor(User user) : base(user)
        {
            InitializeComponent();

            this.viewModel = new UserViewModel(user);
            this.DataContext = viewModel;

            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                this.Title = user.UserName;
            }

            if (!string.IsNullOrEmpty(user.PassWord))
            {
                this.ChangeUserPasswordButton.Visibility = System.Windows.Visibility.Visible;
                this.UserPasswordBox.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (user.ProfilePicturePath == string.Empty || user.ProfilePicturePath == null)
            {
                this.AddUserProfilePictureButton.Visibility = Visibility.Visible;
                this.AddUserProfilePictureButton.IsEnabled = true;
                this.UserProfilePicture.Visibility = Visibility.Collapsed;
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

        private void GroupAutoCompleteBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    if (sender is RadAutoSuggestBox box)
                    {
                        var group = box.DataContext as Group;
                        if (group != null)
                        {
                            if (!Current.UserGroups.Contains(group))
                            {
                                Current.AddUserToGroup(group);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim hinzufügen des Benutzers zur Gruppe: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OrganisationAutoCompleteBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    if (sender is RadAutoCompleteBox box)
                    {
                        var organisation = box.SelectedItem as Organisation;
                        if (organisation != null)
                        {
                            if (!Current.UserOrganisations.Contains(organisation))
                            {
                                Current.AddUserToOrganisation(organisation);
                                box.SelectedItem = null;
                                box.SelectedItems = null;
                                box.SearchText = string.Empty;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim hinzufügen des Benutzers zur Organisation: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddUserProfilePictureButton_Click(object sender, RoutedEventArgs e)
        {
            if (Current.SetUserProfilePicture() == true)
            {
                this.AddUserProfilePictureButton.Visibility = Visibility.Collapsed;
                this.AddUserProfilePictureButton.IsEnabled = false;
                this.UserProfilePicture.Visibility = Visibility.Visible;
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
            if (e.Suggestion is Organisation organisation && organisation != null)
            {
                if (!Current.UserOrganisations.Contains(organisation))
                {
                    Current.AddUserToOrganisation(organisation);
                }
            }
        }

        private void GroupAutoCompleteBox_QuerySubmitted(object sender, Telerik.Windows.Controls.AutoSuggestBox.QuerySubmittedEventArgs e)
        {
            if (e.Suggestion is Group group && group != null)
            {
                if (!Current.UserGroups.Contains(group))
                {
                    Current.AddUserToGroup(group);
                }
            }
        }
    }
}
