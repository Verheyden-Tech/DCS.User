using DCS.CoreLib.View;
using DCS.Resource;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommonServiceLocator;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for OrganisationManagement.xaml
    /// </summary>
    public partial class OrganisationManagement : DefaultAppControl
    {
        private readonly IIconService iconService = ServiceLocator.Current.GetInstance<IIconService>();
        private readonly IOrganisationService organisationService = ServiceLocator.Current.GetInstance<IOrganisationService>();

        private ObservableCollection<Organisation> Organisations { get; set; }
        private OrganisationViewModel viewModel;

        public OrganisationManagement(Organisation organisation)
        {
            InitializeComponent();

            SetContextMenu();

            Organisations = organisationService.GetAll();
            OrganisationGridView.ItemsSource = Organisations;

            var obj = new Organisation();
            viewModel = new OrganisationViewModel(obj);
            this.DataContext = viewModel;
        }

        private void SetContextMenu()
        {
            MenuItem newOrganisation = new MenuItem()
            {
                Header = "Neue Organisation",
                Icon = iconService.GetImage("add_16x.png") // Beispiel-Icon
            };
            newOrganisation.Click += NewOrganisation_Click;

            MenuItem editOrganisation = new MenuItem()
            {
                Header = "Organisation bearbeiten",
                Icon = iconService.GetImage("edit_16x.png") // Beispiel-Icon
            };
            editOrganisation.Click += EditOrganisation_Click;

            MenuItem deleteOrganisation = new MenuItem()
            {
                Header = "Organisation löschen",
                Icon = iconService.GetImage("remove_16x.png") // Beispiel-Icon
            };
            deleteOrganisation.Click += DeleteOrganisation_Click;

            OrganisationManagementContextMenu.Items.Add(newOrganisation);
            OrganisationManagementContextMenu.Items.Add(editOrganisation);
            OrganisationManagementContextMenu.Items.Add(deleteOrganisation);
        }

        private void NewOrganisation_Click(object sender, RoutedEventArgs e)
        {
            var newOrganisation = new Organisation();
            var editor = new OrganisationEditor(newOrganisation);
            if (editor.ShowDialog() == true)
            {
                Organisations.Add(newOrganisation);
            }
        }

        private void EditOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisationGridView.SelectedItem is Organisation selectedOrganisation)
            {
                var editor = new OrganisationEditor(selectedOrganisation);
                editor.ShowDialog();
            }
        }

        private void DeleteOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisationGridView.SelectedItem is Organisation selectedOrganisation)
            {
                if (MessageBox.Show($"Möchten Sie die Organisation '{selectedOrganisation.Name}' wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    organisationService.Delete(selectedOrganisation.Guid);
                    Organisations.Remove(selectedOrganisation);
                }
            }
        }

        private void CreateNewOrganisationButton_Click(object sender, RoutedEventArgs e)
        {
            NewOrganisation_Click(sender, e);
        }
    }
}
