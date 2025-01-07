using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace UserLibrary
{
    public class UserManagementService : IUserManagementService
    {
        #region Private Member
        private ObservableCollection<User> AccUserList = new ObservableCollection<User>();
        private const string filePathUser = "users.json";
        private ObservableCollection<Company> CompanyList = new ObservableCollection<Company>();
        private const string filePathCompany = "companys.json";
        #endregion

        /// <summary>
        /// User-Management repository, contains methods for User-Account management.
        /// </summary>
        public UserManagementService() 
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
        public bool Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city)
        {
            User newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = username,
                PassWord = password,
                IsAdmin = isAdmin,
                Owner = owner,
                FirstName = firstName,
                LastName = lastName,
                Email = eMail,
                Adress = adress,
                City = city
            };

            if(AccUserList.Contains(newUser))
            {
                return false;
            }

            AccUserList.Add(newUser);
            SaveUsers();
            return true;
        }

        public bool BaseRegisterUser(string username, string password, bool isAdmin)
        {
            User newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = username,
                PassWord = password,
                IsAdmin = isAdmin
            };

            if(AccUserList.Contains(newUser))
            {
                return false;
            }

            AccUserList.Add(newUser);
            SaveUsers();
            return true;
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
        public bool RegisterCompany(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype)
        {
            Company newCompany = new Company
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Contact = contact,
                Phone = phone,
                Email = email,
                Adress = adress,
                City = city,
                Postalcode = postalcode,
                Companytype = companytype
            };

            if(CompanyList.Contains(newCompany))
            {
                return false;
            }

            CompanyList.Add(newCompany);
            SaveCompany();
            return true;
        }

        /// <summary>
        /// Load user accounts from json file.
        /// </summary>
        public ObservableCollection<User> LoadUsers()
        {
            if (File.Exists(filePathUser))
            {
                string json = File.ReadAllText(filePathUser);
                AccUserList = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            }
            else
            {
                AccUserList = new ObservableCollection<User>();
            }
            return AccUserList;
        }

        /// <summary>
        /// Save user accounts to json file.
        /// </summary>
        public bool SaveUsers()
        {
            string json = JsonConvert.SerializeObject(AccUserList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePathUser, json);
            return true;
        }

        /// <summary>
        /// Load companys from json file.
        /// </summary>
        public ObservableCollection<Company> LoadCompanys()
        {
            if (File.Exists(filePathCompany))
            {
                string json = File.ReadAllText(filePathCompany);
                CompanyList = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
            }
            else
            {
                CompanyList = new ObservableCollection<Company>();
            }
            return CompanyList;
        }

        /// <summary>
        /// Save company to json file.
        /// </summary>
        public bool SaveCompany()
        {
            string json = JsonConvert.SerializeObject(CompanyList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePathCompany, json);
            return true;
        }
    }
}
