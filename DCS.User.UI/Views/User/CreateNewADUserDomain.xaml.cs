using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for CreateNewADUserDomain.xaml
    /// </summary>
    public partial class CreateNewADUserDomain : Window
    {
        private ADUserViewModel viewModel;

        private ADUser newDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewADUserDomain"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the data context for the view by creating a new instance of 
        /// <see cref="ADUser"/> and initializing the associated <see cref="ADUserViewModel"/>.</remarks>
        public CreateNewADUserDomain()
        {
            InitializeComponent();

            var obj = new ADUser();
            this.viewModel = new ADUserViewModel(obj);
            this.DataContext = this.viewModel;

            NewDomain = obj;
        }

        /// <summary>
        /// Gets the current <see cref="ADUserViewModel"/> instance associated with the data context.
        /// </summary>
        public ADUserViewModel Current
        {
            get
            {
                return DataContext as ADUserViewModel;
            }
        }

        private void CreateDomainButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DomainNameTextBox.Text))
            {
                if (viewModel.CreateNewADUser())
                {
                    NewDomain = viewModel.Model;

                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create the AD User. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Gets or sets the new createt domain.
        /// </summary>
        public ADUser NewDomain
        {
            get { return newDomain; }
            set { newDomain = value; }
        }
    }
}
