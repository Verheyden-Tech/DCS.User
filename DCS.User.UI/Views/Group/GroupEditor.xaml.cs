using DCS.CoreLib.BaseClass;
using DCS.CoreLib.View;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for GroupEditor.xaml
    /// </summary>
    public partial class GroupEditor : DefaultEditorWindow
    {
        private GroupViewModel viewModel;

        private ObservableCollection<User> GroupMember = new ObservableCollection<User>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEditor"/> class with the specified group and view model.
        /// </summary>
        public GroupEditor() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEditor"/> class with the specified group.
        /// </summary>
        /// <remarks>If the provided group has a non-empty name, it is used as the title of the editor.
        /// The data context is set to a new instance of <see cref="GroupViewModel"/> initialized with the specified
        /// group. Additionally, the group members are retrieved and stored if available.</remarks>
        /// <param name="group">The group to be edited. Must not be <see langword="null"/> and should have a valid name.</param>
        public GroupEditor(Group group) : base(group)
        {
            if (!string.IsNullOrWhiteSpace(group.Name))
            {
                this.Title = group.Name;
            }

            viewModel = new GroupViewModel(group);
            this.DataContext = viewModel;

            if (viewModel.GetAllGroupMember(group) != null && viewModel.GetAllGroupMember(group).Count >= 0)
                GroupMember = viewModel.GetAllGroupMember(group);
        }

        /// <summary>
        /// Returns the current instance of the <see cref="GroupViewModel"/> class as DataContext.
        /// </summary>
        public GroupViewModel Current
        {
            get
            {
                return DataContext as GroupViewModel;
            }
        }

        private void AddUserSuggestBox_QuerySubmitted(object sender, Telerik.Windows.Controls.AutoSuggestBox.QuerySubmittedEventArgs e)
        {
            if (e.Suggestion is User user)
            {
                if (!GroupMember.Contains(user))
                {
                    if (viewModel.AddUserToGroup(user))
                        GroupMember.Add(user);
                }

                AddUserSuggestBox.Text = string.Empty;
            }
        }

        private void RemoveItemButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is ListViewItem item && item.DataContext is User user)
            {
                if (GroupMember.Contains(user))
                {
                    if (viewModel.RemoveUserFromGroup(user))
                        GroupMember.Remove(user);
                }
            }
        }
    }
}
