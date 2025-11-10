using System.Collections.ObjectModel;
using System.Globalization;

namespace DCS.User
{
    /// <summary>
    /// Provides a thread-safe service for managing the application's current language and associated settings.
    /// </summary>
    /// <remarks>This class implements a singleton pattern to ensure a single instance is used throughout the
    /// application. It allows setting and retrieving the current language and flag, as well as retrieving a list of
    /// available languages. Once the current language or flag is set, it cannot be changed unless explicitly reset
    /// using the <see cref="UnsetLanguage"/> method.</remarks>
    public class CurrentLanguageService
    {
        private static readonly Lazy<CurrentLanguageService> _instance = new(() => new CurrentLanguageService());
        private static readonly object _lock = new();
        private bool isLocked;

        /// <summary>
        /// Gets the singleton instance of the <see cref="CurrentLanguageService"/> class.
        /// </summary>
        public static CurrentLanguageService Instance => _instance.Value;

        private string currentLanguage;
        private string currentFlag;

        /// <summary>
        /// Gets or sets the current language for the application.
        /// </summary>
        /// <remarks>The property is thread-safe. Attempting to set the language after it has already been
        /// set will result in an error being logged. To change the current language the user should use the <see cref="UnsetLanguage"/> method and set a new language with the <see cref="SetLanguage(string)"/> method.</remarks>
        public string CurrentLanguage
        {
            get
            {
                lock (_lock)
                {
                    return currentLanguage;
                }
            }
            set
            {
                lock (_lock)
                {
                    if (!isLocked)
                    {
                        currentLanguage = value;
                        isLocked = true;
                    }
                    else
                    {
                        Log.LogManager.Singleton.Error("Current language is already set and cannot be changed.", "Setting current language");
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the current flag value.
        /// </summary>
        /// <remarks>The property is thread-safe. If an attempt is made to set the value after it has
        /// already been set,  an error will be logged, and the value will remain unchanged.</remarks>
        public string CurrentFlag
        {
            get
            {
                lock (_lock)
                {
                    return currentFlag;
                }
            }
            set
            {
                lock (_lock)
                {
                    if (!isLocked)
                    {
                        currentFlag = value;
                        isLocked = true;
                    }
                    else
                    {
                        Log.LogManager.Singleton.Error("Current flag is already set and cannot be changed.", "Setting current flag");
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a list of all available language codes supported by the system.
        /// </summary>
        /// <remarks>The method returns an array of unique language codes in the format of culture names
        /// (e.g., "en-US", "fr-FR")  that represent specific cultures. Neutral cultures and cultures with empty or
        /// whitespace-only names are excluded.</remarks>
        /// <returns>An array of strings containing the language codes of all available specific cultures.  The array will be
        /// empty if no specific cultures are available.</returns>
        public ObservableCollection<CultureInfo> GetAvailableLanguages()
        {
            ObservableCollection<CultureInfo> cultures = new ObservableCollection<CultureInfo>(
                System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures)
                    .Where(c => !c.IsNeutralCulture && !string.IsNullOrWhiteSpace(c.DisplayName) && !string.IsNullOrWhiteSpace(c.TwoLetterISOLanguageName))
                    .Distinct());

            return cultures;
        }

        /// <summary>
        /// Sets the current language for the application.
        /// </summary>
        /// <param name="language">The language to set. This value cannot be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="language"/> is <see langword="null"/>.</exception>
        public void SetLanguage(string language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var culture = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures)
                .First(c => c.DisplayName.Equals(language, StringComparison.OrdinalIgnoreCase));

            CurrentLanguage = culture.DisplayName;
            CurrentFlag = culture.TwoLetterISOLanguageName;
        }

        /// <summary>
        /// Resets the current language setting to an empty string and unlocks the language configuration.
        /// </summary>
        /// <remarks>This method clears the current language setting and sets the internal lock state to
        /// allow further modifications. It is thread-safe and ensures that the operation is performed
        /// atomically.</remarks>
        public void UnsetLanguage()
        {
            lock (_lock)
            {
                currentLanguage = string.Empty;
                isLocked = false;
            }
        }
    }
}
