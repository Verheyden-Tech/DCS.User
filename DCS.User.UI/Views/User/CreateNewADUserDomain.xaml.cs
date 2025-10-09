using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for CreateNewADUserDomain.xaml
    /// </summary>
    public partial class CreateNewADUserDomain : Window
    {
        private ADUserViewModel viewModel;

        public CreateNewADUserDomain()
        {
            InitializeComponent();

            var obj = new ADUser();
            this.viewModel = new ADUserViewModel(obj);
            this.DataContext = this.viewModel;
        }

        public ADUserViewModel Current
        {
            get
            {
                return DataContext as ADUserViewModel;
            }
        }

        private void CreateDomainButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(DomainNameTextBox.Text))
            {

            }
        }
    }
}
