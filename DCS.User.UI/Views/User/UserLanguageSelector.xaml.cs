using DCS.CoreLib.View;
using DCS.Localization;
using DCS.Resource;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserLanguageSelector.xaml
    /// </summary>
    public partial class UserLanguageSelector : DefaultMainWindow 
    {
        private readonly ILocalizationService localizationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILocalizationService>();
        private UserViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLanguageSelector"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the necessary components for the <see
        /// cref="UserLanguageSelector"/> control. Ensure that the control is properly added to the user interface
        /// before use.</remarks>
        public UserLanguageSelector()
        {
            InitializeComponent();

            var obj = new User();
            viewModel = new UserViewModel(obj);
            DataContext = viewModel;
        }

        private void LanguageListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(LanguageListView.SelectedItem != null && LanguageListView.SelectedItem is System.Globalization.CultureInfo culture)
            {
                CurrentSessionService.Instance.SetCurrentUserCulture(culture);
                DialogResult = true;
                Close();
            }
        }

        private void RadAutoSuggestBox_SuggestionChosen(object sender, Telerik.Windows.Controls.AutoSuggestBox.SuggestionChosenEventArgs e)
        {
            if(e.Suggestion != null && e.Suggestion is System.Globalization.CultureInfo culture)
            {
                if (localizationService.SetLanguage(culture.Name))
                {
                    CurrentSessionService.Instance.SetCurrentUserCulture(culture);
                    DialogResult = true;
                    Close();
                }
                
                Log.LogManager.Singleton.Info($"Failed to changed application language to {culture.DisplayName}", "UserLanguageSelector");
                MessageBox.Show(localizationService.Translate("ErrorChangingLanguage") + $"{culture.DisplayName}", localizationService.Translate("Error"), MessageBoxButton.OK);
                return;
            }
        }
    }
}
