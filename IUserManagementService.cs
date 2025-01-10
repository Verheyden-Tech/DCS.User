using System.Collections.ObjectModel;

namespace UserLibrary
{
    /// <summary>
    /// UserManagementService interface.
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Bool for user login, checks username and password in json file.
        /// </summary>
        /// <returns>Wether the login was succesfull.</returns>
        User CheckLogin(string username, string password);

        /// <summary>
        /// Register new user-account to json file.
        /// </summary>
        /// <returns>User instance.</returns>
        User CreateFullUser(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city);

        /// <summary>
        /// Basic register User.
        /// </summary>
        /// <returns>User instance.</returns>
        User CreateBaseUser(string username, string password, bool isAdmin);

        /// <summary>
        /// Load user accounts from json file.
        /// </summary>
        ObservableCollection<User> LoadUsers(string filePath);

        /// <summary>
        /// Register new company to json file.
        /// </summary>
        Company RegisterCompany(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype);

        /// <summary>
        /// Save company to json file.
        /// </summary>
        ObservableCollection<Company> LoadCompanys(string filePath);
    }
}
