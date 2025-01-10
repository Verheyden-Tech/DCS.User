using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace UserLibrary
{
    public class UserManagementService : IUserManagementService
    {
        /// <summary>
        /// User-Management repository, contains methods for User-Account management.
        /// </summary>
        public UserManagementService() 
        {
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
        public ObservableCollection<User> LoadUserData(string json)
        {
            var token = JToken.Parse(json);

            if (token.Type == JTokenType.Array)
            {
                return JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
            }
            else if (token.Type == JTokenType.Object)
            {
                var singleUser = JsonConvert.DeserializeObject<User>(json);
                return new ObservableCollection<User> { singleUser };
            }
            else
            {
                throw new InvalidOperationException("Unerwartetes JSON-Format.");
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ObservableCollection<Company> LoadCompanys(string json)
        {
            var token = JToken.Parse(json);

            if (token.Type == JTokenType.Array)
            {
                return JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
            }
            else if (token.Type == JTokenType.Object)
            {
                var singleCompany = JsonConvert.DeserializeObject<Company>(json);
                return new ObservableCollection<Company> { singleCompany };
            }
            else
            {
                throw new InvalidOperationException("Unerwartetes JSON-Format.");
            }
        }
    }
}
