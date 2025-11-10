using DCS.CoreLib.View;
using System.Windows;

namespace DCS.User.UI
{
    /// <summary>
    /// Interaction logic for UserLanguageSelector.xaml
    /// </summary>
    public partial class UserLanguageSelector : DefaultMainWindow 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLanguageSelector"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the necessary components for the <see
        /// cref="UserLanguageSelector"/> control. Ensure that the control is properly added to the user interface
        /// before use.</remarks>
        public UserLanguageSelector()
        {
            InitializeComponent();
        }

        private void LanguageListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(LanguageListView.SelectedItem != null && LanguageListView.SelectedItem is System.Globalization.CultureInfo culture)
            {
                CurrentLanguageService.Instance.SetLanguage(culture.DisplayName);
                DialogResult = true;
                Close();
            }
        }

        private void RadAutoSuggestBox_SuggestionChosen(object sender, Telerik.Windows.Controls.AutoSuggestBox.SuggestionChosenEventArgs e)
        {
            if(e.Suggestion != null && e.Suggestion is System.Globalization.CultureInfo culture)
            {
                CurrentLanguageService.Instance.SetLanguage(culture.DisplayName);
                DialogResult = true;
                Close();
            }
        }
    }
}
