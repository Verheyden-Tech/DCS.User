using CommonServiceLocator;
using DCS.CoreLib.View;
using DCS.Localization;
using System.Collections.ObjectModel;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for RoleManagement.xaml
    /// </summary>
    public partial class RoleManagement : DcsInternPage
    {
        private readonly ILocalizationService localizationService = ServiceLocator.Current.GetInstance<ILocalizationService>();
        private readonly IRoleService roleService = ServiceLocator.Current.GetInstance<IRoleService>();

        private ObservableCollection<Role> Roles { get; set; }
        private RoleViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManagement"/> class with the specified role.
        /// </summary>
        public RoleManagement()
        {
            InitializeComponent();

            Title = localizationService.Translate("RoleManagement");
            DisplayName = localizationService.Translate("RoleManagement");
            Name = "RoleManagement";
            GroupName = "User";

            Roles = new ObservableCollection<Role>();
            Roles = roleService.GetAll();
            RoleGridView.ItemsSource = Roles;

            var obj = new Role();
            viewModel = new RoleViewModel(obj);
            this.DataContext = viewModel;
        }

        private void GenerateRoles_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewRole_Click(object sender, RoutedEventArgs e)
        {
            var editor = new RoleEditor();
            if (editor.ShowDialog() == true)
            {
                RoleGridView.Items.Refresh();
            }
        }

        private void EditRole_Click(object sender, RoutedEventArgs e)
        {
            if(RoleGridView.SelectedItems != null)
            {
                var editor = new RoleEditor();
                editor.Edit(RoleGridView.SelectedItems);
                if (editor.ShowDialog() == true)
                {
                    RoleGridView.Items.Refresh();
                }
            }
        }

        private void DeleteRole_Click(object sender, RoutedEventArgs e)
        {
            if (RoleGridView.SelectedItems != null)
            {
                if (MessageBox.Show($"Möchten Sie die Rolle/-n wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach (Role role in RoleGridView.SelectedItems)
                    {
                        if (!roleService.Delete(role.Guid))
                        {
                            MessageBox.Show(localizationService.Translate("ErrorOccurred") + $": {role.Name}", localizationService.Translate("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
            }
        }

        private void CreateNewRoleButton_Click(object sender, RoutedEventArgs e)
        {
            NewRole_Click(sender, e);
        }
    }
}
