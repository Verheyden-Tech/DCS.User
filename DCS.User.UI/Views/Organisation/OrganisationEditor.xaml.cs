using DCS.CoreLib.View;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for OrganisationEditor.xaml
    /// </summary>
    public partial class OrganisationEditor : DefaultEditorWindow
    {
        private OrganisationViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationEditor"/> class.
        /// </summary>
        /// <param name="organisation">Instance of <see cref="Organisation"/>.</param>
        public OrganisationEditor(Organisation organisation) : base(organisation)
        {
            InitializeComponent();
            
            viewModel = new OrganisationViewModel(organisation);
            this.DataContext = viewModel;

            if (!string.IsNullOrWhiteSpace(organisation.Name))
            {
                this.Title = organisation.Name;
            }
        }

        /// <summary>
        /// Returns the current instance of the <see cref="OrganisationViewModel"/> class as DataContext.
        /// </summary>
        public OrganisationViewModel Current
        {
            get
            {
                return DataContext as OrganisationViewModel;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Save())
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Fehler beim Speichern der Organisation.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}