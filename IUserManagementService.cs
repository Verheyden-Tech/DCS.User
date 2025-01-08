using System.Collections.ObjectModel;

namespace UserLibrary
{
    /// <summary>
    /// Interface for UserManagementService.
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Bool for user login, checks username and password in json file.
        /// <inheritdoc cref="UserManagementService.Login(string, string)"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Wether the login was succesfull.</returns>
        bool Login(string username, string password);

        /// <summary>
        /// Register new user-account to json file.
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
        User Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city);

        User BaseRegisterUser(string username, string password, bool isAdmin);

        /// <summary>
        /// Load user accounts from json file.
        /// <inheritdoc cref="UserManagementService.LoadUsers"/>
        /// </summary>
        ObservableCollection<User> LoadUsers();

        /// <summary>
        /// Save user accounts to json file.
        /// <inheritdoc cref="UserManagementService.SaveUsers"/>
        /// </summary>
        bool SaveUsers();

        /// <summary>
        /// Register new company to json file.
        /// <inheritdoc cref="UserManagementService.RegisterCompany(string, string, string, string, string, string, string, string)"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contact"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="adress"></param>
        /// <param name="city"></param>
        /// <param name="postalcode"></param>
        /// <param name="companytype"></param>
        Company RegisterCompany(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype);

        /// <summary>
        /// Save company to json file.
        /// <inheritdoc cref="UserManagementService.LoadCompanys"/>
        /// </summary>
        ObservableCollection<Company> LoadCompanys();

        /// <summary>
        /// Load companys from json file.
        /// <inheritdoc cref="UserManagementService.SaveCompany"/>
        /// </summary>
        bool SaveCompany();
    }
}
