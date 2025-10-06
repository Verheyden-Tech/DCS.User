using DCS.CoreLib.View;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for OrganisationEditor.xaml
    /// </summary>
    public partial class OrganisationEditor : DefaultEditorWindow
    {
        private OrganisationViewModel viewModel;

        private ObservableCollection<User> OrganisationMember = new ObservableCollection<User>();

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

            if(viewModel.GetAllOrganisationMember(organisation) != null && viewModel.GetAllOrganisationMember(organisation).Count >= 0)
                OrganisationMember = viewModel.GetAllOrganisationMember(organisation);
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

        private void AddUserSuggestBox_QuerySubmitted(object sender, Telerik.Windows.Controls.AutoSuggestBox.QuerySubmittedEventArgs e)
        {
            if(e.Suggestion is User user)
            {
                if(!OrganisationMember.Contains(user))
                {
                    if(viewModel.AddUserToOrganisation(user))
                        OrganisationMember.Add(user);
                }

                AddUserSuggestBox.Text = string.Empty;
            }
        }

        private void RemoveItemButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(sender is ListViewItem item && item.DataContext is User user)
            {
                if(OrganisationMember.Contains(user))
                {
                    if(viewModel.RemoveUserFromOrganisation(user))
                        OrganisationMember.Remove(user);
                }
            }
        }
    }
}