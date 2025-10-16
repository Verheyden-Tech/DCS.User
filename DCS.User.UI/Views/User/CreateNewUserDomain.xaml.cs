using DCS.CoreLib.View;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for CreateNewADUserDomain.xaml
    /// </summary>
    public partial class CreateNewUserDomain : DefaultMainWindow
    {
        private UserDomainViewModel viewModel;

        private UserDomain newDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserDomain"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the data context for the view by creating a new instance of 
        /// <see cref="UserDomain"/> and initializing the associated <see cref="UserDomainViewModel"/>.</remarks>
        public CreateNewUserDomain()
        {
            InitializeComponent();

            var obj = new UserDomain();
            this.viewModel = new UserDomainViewModel(obj);
            this.DataContext = this.viewModel;

            NewDomain = obj;
        }

        /// <summary>
        /// Gets the current <see cref="UserDomainViewModel"/> instance associated with the data context.
        /// </summary>
        public UserDomainViewModel Current
        {
            get
            {
                return DataContext as UserDomainViewModel;
            }
        }

        private void CreateDomainButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DomainNameTextBox.Text))
            {
                if (viewModel.CreateNewDomain())
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
        public UserDomain NewDomain
        {
            get { return newDomain; }
            set { newDomain = value; }
        }
    }
}
