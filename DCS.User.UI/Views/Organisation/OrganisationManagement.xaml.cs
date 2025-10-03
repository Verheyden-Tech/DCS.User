using CommonServiceLocator;
using DCS.CoreLib.View;
using DCS.Resource;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationManagement"/> class with the specified
        /// organisation.
        /// </summary>
        public OrganisationManagement()
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
            var glyphDelete = new RadGlyph() { Glyph = "\te10C", FontSize = 16, Foreground = Brushes.Black };
            var glyphEdit = new RadGlyph() { Glyph = "\te10B", FontSize = 16, Foreground = Brushes.Black };
            var glyphAdd = new RadGlyph() { Glyph = "\te11E", FontSize = 16, Foreground = Brushes.Black };
            var glyphGenerate = new RadGlyph() { Glyph = "\te13B", FontSize = 16, Foreground = Brushes.Black };

            MenuItem newOrganisation = new MenuItem()
            {
                Header = "Neue Organisation",
                Icon = glyphAdd
            };
            newOrganisation.Click += NewOrganisation_Click;

            MenuItem editOrganisation = new MenuItem()
            {
                Header = "Organisation bearbeiten",
                Icon = glyphEdit
            };
            editOrganisation.Click += EditOrganisation_Click;

            MenuItem deleteOrganisation = new MenuItem()
            {
                Header = "Organisation löschen",
                Icon = glyphDelete
            };
            deleteOrganisation.Click += DeleteOrganisation_Click;

            MenuItem generateOrganisations = new MenuItem()
            {
                Header = "Test Organisationen generieren",
                Icon = glyphGenerate
            };
            generateOrganisations.Click += GenerateOrganisations_Click;

            OrganisationManagementContextMenu.Items.Add(newOrganisation);
            OrganisationManagementContextMenu.Items.Add(editOrganisation);
            OrganisationManagementContextMenu.Items.Add(deleteOrganisation);
        }

        private void GenerateOrganisations_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
