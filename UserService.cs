using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace UserLibrary
{
    public class UserService
    {
        private static readonly UserService instance = new UserService();

        public static UserService Singleton
        {
            get { return instance; }
        }

        /// <summary>
        /// UserService contains methods for User-Account management.
        /// </summary>
        private UserService() 
        {
        }

        /// <summary>
        /// Creates new user instance.
        /// </summary>
        /// <returns>User instance.</returns>
        public User CreateFullUser(string username, string password, bool isAdmin, string firstName = "", string lastName = "", string eMail = "", string adress = "", string city = "")
        {
            User newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = username,
                PassWord = password,
                IsAdmin = isAdmin,
                Owner = username,
                FirstName = firstName,
                LastName = lastName,
                Email = eMail,
                Adress = adress,
                City = city
            };
            return newUser;
        }

        /// <summary>
        /// Creates new user with basic account informations.
        /// </summary>
        /// <returns>Base user instance.</returns>
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
        /// Creates a new company instance.
        /// </summary>
        /// <returns>Company instance.</returns>
        public Company CreateCompany(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype)
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
        /// Creates a new customer instance.
        /// </summary>
        /// <returns>Customer instance.</returns>
        public Customer CreateCustomer(string firstName = "", string lastName = "", string mailAddress = "", int phoneNumber = default, string adress = "", string city = "", int postalCode = default, Company? company = null, bool isActive = true)
        {
            Customer newCustomer = new Customer
            {
                Guid = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MailAddress = mailAddress,
                PhoneNumber = phoneNumber,
                Address = adress,
                City = city,
                PostalCode = postalCode,
                Company = company,
                IsActive = isActive
            };
            return newCustomer;
        }

        /// <summary>
        /// Get local user data from json file.
        /// </summary>
        /// <param name="json">Json File with user data.</param>
        /// <returns>List with avialable users.</returns>
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
        /// Get local company data from json file.
        /// </summary>
        /// <param name="json">Json File with user data.</param>
        /// <returns>List with avialable companys.</returns>
        public ObservableCollection<Company> LoadCompanyData(string json)
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

        /// <summary>
        /// Get local customer data from json file.
        /// </summary>
        /// <param name="json">Json File with user data.</param>
        /// <returns>List with avialable customers.</returns>
        public ObservableCollection<Customer> LoadCustomerData(string json)
        {
            var token = JToken.Parse(json);

            if (token.Type == JTokenType.Array)
            {
                return JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);
            }
            else if (token.Type == JTokenType.Object)
            {
                var singleCompany = JsonConvert.DeserializeObject<Customer>(json);
                return new ObservableCollection<Customer> { singleCompany };
            }
            else
            {
                throw new InvalidOperationException("Unerwartetes JSON-Format.");
            }
        }
    }
}
