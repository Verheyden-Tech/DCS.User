using DCS.CoreLib.BaseClass;
using DCS.CoreLib.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for GroupManagement.xaml
    /// </summary>
    public partial class GroupManagement : DefaultAppControl
    {
        private readonly IGroupService groupService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IGroupService>();

        private ObservableCollection<Group> Groups { get; set; }
        private GroupViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupManagement"/> class with the specified group.
        /// </summary>
        public GroupManagement()
        {
            InitializeComponent();

            Groups = new ObservableCollection<Group>();
            Groups = groupService.GetAll();
            GroupGridView.ItemsSource = Groups;

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
            if (GroupGridView.SelectedItem is Group selectedGroup)
            {
                var editor = new GroupEditor(selectedGroup);
                editor.ShowDialog();
            }
        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupGridView.SelectedItem is Group selectedGroup)
            {
                if (MessageBox.Show($"Möchten Sie die Rolle '{selectedGroup.Name}' wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
