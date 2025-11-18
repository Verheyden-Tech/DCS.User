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
            Name = "GroupManagement";
            GroupName = "User";

            Groups = new ObservableCollection<Group>();
            Groups = groupService.GetAll();
            GroupGridView.ItemsSource = Groups;

            var obj = new Group();
            viewModel = new GroupViewModel(obj);
            this.DataContext = viewModel;
        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            var editor = new GroupEditor();
            if (editor.ShowDialog() == true)
            {
                GroupGridView.Items.Refresh();
            }
        }

        private void EditGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupGridView.SelectedItems != null)
            {
                var editor = new GroupEditor();
                editor.Edit(GroupGridView.SelectedItems);
                if (editor.ShowDialog() == true) 
                {
                    GroupGridView.Items.Refresh();
                }
            }
        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupGridView.SelectedItems != null)
            {
                if (MessageBox.Show(localizationService.Translate("ConfirmDeleteGroup"), localizationService.Translate("Delete"), MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach(Group group in GroupGridView.SelectedItems)
                    {
                        if (!groupService.Delete(group.Guid))
                        {
                            MessageBox.Show(localizationService.Translate("ErrorOccurred"), localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
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
