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
            Name = "OrganisationManagement";
            GroupName = "User";

            Organisations = organisationService.GetAll();
            OrganisationGridView.ItemsSource = Organisations;

            var obj = new Organisation();
            viewModel = new OrganisationViewModel(obj);
            DataContext = viewModel;
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
                editor.Edit(OrganisationGridView.SelectedItems);
                if(editor.ShowDialog() == true)
                    OrganisationGridView.Items.Refresh();
            }
        }

        private void DeleteOrganisation_Click(object sender, RoutedEventArgs e)
        {
            if (OrganisationGridView.SelectedItems != null)
            {
                if (MessageBox.Show(localizationService.Translate("ConfirmDeleteOrganisation"), localizationService.Translate("Delete"), MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach(Organisation organisation in OrganisationGridView.SelectedItems)
                    {
                        if (!organisationService.Delete(organisation.Guid))
                        {
                            MessageBox.Show(localizationService.Translate("ErrorOccurred") + $": {organisation.Name}", localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
            }
        }

        private void CreateNewOrganisationButton_Click(object sender, RoutedEventArgs e)
        {
            NewOrganisation_Click(sender, e);
        }
    }
}
