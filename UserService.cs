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
        public User CreateFullUser(string username, string password, bool isAdmin = false, string firstName = "", string lastName = "", string eMail = "", string adress = "", string city = "")
        {
            User newUser = new User
            {
                Guid = Guid.NewGuid(),
                UserName = username,
                PassWord = password,
                IsAdmin = isAdmin,
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
        public User CreateBaseUser(string username, string password, bool isAdmin = false)
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
        public Company CreateCompany(string name, string contact = "", string phone = "", string email = "", string adress = "", string city = "", string postalcode = "", string companytype = "")
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
        public Contact CreateContact(string firstName = "", string lastName = "", string mailAddress = "", int phoneNumber = default, string adress = "", string city = "", int postalCode = default, string dataOwner = "", string? company = "", bool isActive = true)
        {
            Contact newContact = new Contact
            {
                Guid = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MailAddress = mailAddress,
                PhoneNumber = phoneNumber,
                Address = adress,
                City = city,
                PostalCode = postalCode,
                DataOwner = dataOwner,
                Company = company,
                IsActive = isActive
            };
            return newContact;
        }

        public Group CreateGroup(string name, string description = "", bool isActive  = true)
        {
            Group newGroup = new Group
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Description = description,
                IsActive = isActive
            };
            return newGroup;
        }

        public Role CreateRole(string roleName, string description = "", bool isAdmin = false, bool isActive = true)
        {
            Role newRole = new Role
            {
                Guid = Guid.NewGuid(),
                Name = roleName,
                Description = description,
                IsAdmin = isAdmin,
                IsActive = isActive
            };
            return newRole;
        }
    }
}
