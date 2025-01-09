using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace UserLibrary
{
    public class UserManagementService : IUserManagementService
    {
        private ObservableCollection<User> accUserList;
        private ObservableCollection<Company> companyList;

        /// <summary>
        /// User-Management repository, contains methods for User-Account management.
        /// </summary>
        public UserManagementService() 
        {
            accUserList = new ObservableCollection<User>();
            companyList = new ObservableCollection<Company>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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
        /// <inheritdoc/>
        /// </summary>
        public User CreateFullUser(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city)
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

            return newUser;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public User CreateBaseUser(string username, string password, bool isAdmin)
        {
            User newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = username,
                PassWord = password,
                IsAdmin = isAdmin
            };

            return newUser;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Company RegisterCompany(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype)
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

            return newCompany;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ObservableCollection<User> LoadUsers(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
                foreach(var user in users)
                {
                    accUserList.Add(user);
                }
            }
            return accUserList;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ObservableCollection<Company> LoadCompanys(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                companyList = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
            }
            return companyList;
        }

        #region Lists
        public ObservableCollection<User> AccUserList
        {
            get
            {
                if(accUserList == null)
                    accUserList = new ObservableCollection<User>();
                return accUserList;
            }
            set
            {
                if(accUserList == null)
                {
                    accUserList = new ObservableCollection<User>();
                }
                accUserList = value;
            }
        }

        public ObservableCollection<Company> CompanyList
        {
            get
            {
                if (companyList == null)
                    companyList = new ObservableCollection<Company>();
                return companyList;
            }
            set
            {
                if (companyList == null)
                {
                    companyList = new ObservableCollection<Company>();
                }
                companyList = value;
            }
        }
        #endregion
    }
}
