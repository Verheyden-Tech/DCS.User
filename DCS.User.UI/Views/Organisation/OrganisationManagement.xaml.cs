using CommonServiceLocator;
using DCS.CoreLib.View;
using DCS.Localization;
using System.Collections.ObjectModel;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for OrganisationManagement.xaml
    /// </summary>
    public partial class OrganisationManagement : DcsInternPage
    {
        private readonly ILocalizationService localizationService = ServiceLocator.Current.GetInstance<ILocalizationService>();
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

            Title = localizationService.Translate("OrganisationManagement");
            DisplayName = localizationService.Translate("OrganisationManagement");
            base.Name = "OrganisationManagement";
            base.GroupName = "User";

            Organisations = organisationService.GetAll();
            OrganisationGridView.ItemsSource = Organisations;
            OrganisationGridView.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;

            var obj = new Organisation();
            viewModel = new OrganisationViewModel(obj);
            this.DataContext = viewModel;
        }

        private void GenerateOrganisations_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewOrganisation_Click(object sender, RoutedEventArgs e)
        {
            var editor = new OrganisationEditor();
            if (editor.ShowDialog() == true)
                OrganisationGridView.Items.Refresh();
        }

        private void EditOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisationGridView.SelectedItems != null)
            {
                var editor = new OrganisationEditor();
                editor.AddPagingObjects(OrganisationGridView.SelectedItems);
                if(editor.ShowDialog() == true)
                    OrganisationGridView.Items.Refresh();
            }
        }

        private void DeleteOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisationGridView.SelectedItem is Organisation selectedOrganisation)
            {
                if (MessageBox.Show(localizationService.Translate("ConfirmDeleteOrganisation") + $"{selectedOrganisation.Name}", localizationService.Translate("Delete"), MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
