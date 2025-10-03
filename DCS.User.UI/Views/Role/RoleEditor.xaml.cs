using DCS.CoreLib.View;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RoleEditor.xaml
    /// </summary>
    public partial class RoleEditor : DefaultEditorWindow
    {
        private RoleViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleEditor"/> class.
        /// </summary>
        /// <param name="role">Instance of <see cref="Role"/>.</param>
        public RoleEditor(Role role) : base(role)
        {
            InitializeComponent();

            viewModel = new RoleViewModel(role);
            this.DataContext = viewModel;

            if (!string.IsNullOrWhiteSpace(role.Name))
            {
                this.Title = role.Name;
            }
        }

        /// <summary>
        /// Returns the current instance of the <see cref="RoleViewModel"/> class as DataContext.
        /// </summary>
        public RoleViewModel Current
        {
            get
            {
                return DataContext as RoleViewModel;
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
                MessageBox.Show("Fehler beim Speichern der Rolle.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}