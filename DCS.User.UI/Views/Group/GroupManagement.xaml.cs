using DCS.CoreLib.View;
using DCS.Localization;
using System.Collections.ObjectModel;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for GroupManagement.xaml
    /// </summary>
    public partial class GroupManagement : DcsInternPage
    {
        private readonly ILocalizationService localizationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILocalizationService>();
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();

        private ObservableCollection<Group> Groups { get; set; }
        private GroupViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupManagement"/> class with the specified group.
        /// </summary>
        public GroupManagement()
        {
            InitializeComponent();

            Title = localizationService.Translate("GroupManagement");
            DisplayName = localizationService.Translate("GroupManagement");
            base.Name = "GroupManagement";
            base.GroupName = "User";

            Groups = new ObservableCollection<Group>();
            Groups = groupService.GetAll();
            GroupGridView.ItemsSource = Groups;
            GroupGridView.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;

            var obj = new Group();
            viewModel = new GroupViewModel(obj);
            this.DataContext = viewModel;
        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = new Group();
            var editor = new GroupEditor(newGroup);
            if (editor.ShowDialog() == true)
            {
                Groups.Add(newGroup);
            }
        }

        private void EditGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupGridView.SelectedItems != null)
            {
                var editor = new GroupEditor();
                editor.AddPagingObjects(GroupGridView.SelectedItems);
                if (editor.ShowDialog() == true) 
                {
                    GroupGridView.Items.Refresh();
                }
            }
        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupGridView.SelectedItem is Group selectedGroup)
            {
                if (MessageBox.Show(localizationService.Translate("ConfirmDeleteGroup") + $"{selectedGroup.Name}", localizationService.Translate("Delete"), MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    groupService.Delete(selectedGroup.Guid);
                    Groups.Remove(selectedGroup);
                }
            }
        }

        private void GenerateGroup_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateNewGroupButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewGroup_Click(sender, e);
        }
    }
}
