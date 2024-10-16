using Newtonsoft.Json;

namespace UserLibrary
{
    public class UserManagementRepository : IUserManagementRepository
    {
        #region Private Member
        private List<User> AccUserList = new List<User>();
        private const string filePathUser = "users.json";
        private List<Company> CompanyList = new List<Company>();
        private const string filePathCompany = "companys.json";
        #endregion

        /// <summary>
        /// User-Management repository, contains methods for User-Account management.
        /// </summary>
        public UserManagementRepository() 
        {
            LoadUsers();
            LoadCompanys();
        }

        /// <summary>
        /// Bool for user login, checks username and password in json file.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Wether the login was succesfull.</returns>
        public bool Login(string username, string password)
        {
            var currentUser = AccUserList.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            if (currentUser != null)
            {
                return true;
            }
            return false;
        }

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
        public void Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city)
        {
            AccUserList.Add(new User(username, password, isAdmin, owner, firstName, lastName, eMail, adress, city));
            SaveUsers();
        }

        /// <summary>
        /// Register new company to json file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contact"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="adress"></param>
        /// <param name="city"></param>
        /// <param name="postalcode"></param>
        /// <param name="companytype"></param>
        public void RegisterCompany(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype)
        {
            CompanyList.Add(new Company(name, contact, phone, email, adress, city, postalcode, companytype));
            SaveCompany();
        }

        /// <summary>
        /// Load user accounts from json file.
        /// </summary>
        public void LoadUsers()
        {
            if (File.Exists(filePathUser))
            {
                string json = File.ReadAllText(filePathUser);
                AccUserList = JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                AccUserList = new List<User>();
            }
        }

        /// <summary>
        /// Save user accounts to json file.
        /// </summary>
        public void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(AccUserList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePathUser, json);
        }

        /// <summary>
        /// Load companys from json file.
        /// </summary>
        public void LoadCompanys()
        {
            if (File.Exists(filePathCompany))
            {
                string json = File.ReadAllText(filePathCompany);
                CompanyList = JsonConvert.DeserializeObject<List<Company>>(json);
            }
            else
            {
                CompanyList = new List<Company>();
            }
        }

        /// <summary>
        /// Save company to json file.
        /// </summary>
        public void SaveCompany()
        {
            string json = JsonConvert.SerializeObject(CompanyList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePathCompany, json);
        }
    }
}
