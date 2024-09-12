namespace UserLibrary
{
    /// <summary>
    /// Interface for UserService.
    /// <inheritdoc cref="UserService"/>
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Bool for user login, checks username and password in json file.
        /// <inheritdoc cref="UserService.Login(string, string)"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Wether the login was succesfull.</returns>
        bool Login(string username, string password);

        /// <summary>
        /// Register new user-account to json file.
        /// <inheritdoc cref="UserService.Register(string, string, bool, string, string, string, string, string, string)"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isAdmin"></param>
        /// <param name="owner"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="eMail"></param>
        /// <param name="adress"></param>
        /// <param name="city"></param>
        void Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city);

        /// <summary>
        /// Load user accounts from json file.
        /// <inheritdoc cref="UserService.LoadUsers"/>
        /// </summary>
        void LoadUsers();

        /// <summary>
        /// Save user accounts to json file.
        /// <inheritdoc cref="UserService.SaveUsers"/>
        /// </summary>
        void SaveUsers();

        /// <summary>
        /// Register new company to json file.
        /// <inheritdoc cref="UserService.RegisterCompany(string, string, string, string, string, string, int, string)"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contact"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="adress"></param>
        /// <param name="city"></param>
        /// <param name="postalcode"></param>
        /// <param name="companytype"></param>
        void RegisterCompany(string name, string contact, string phone, string email, string adress, string city, int postalcode, string companytype);

        /// <summary>
        /// Save company to json file.
        /// <inheritdoc cref="UserService.SaveCompany"/>
        /// </summary>
        void SaveCompany();

        /// <summary>
        /// Load companys from json file.
        /// <inheritdoc cref="UserService.LoadCompanys"/>
        /// </summary>
        void LoadCompanys();
    }
}
