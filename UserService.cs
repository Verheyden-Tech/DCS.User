namespace UserLibrary
{
    public class UserService : IUserService
    {

        private IUserManagementRepository repository;

        /// <summary>
        /// Userservice for user repository.
        /// <inheritdoc cref="UserManagementRepository"/>
        /// </summary>
        public UserService()
        {
            repository = new UserManagementRepository();
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.Login(string, string)"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Wether the login was succesfull.</returns>
        public bool Login(string username, string password)
        {
            return repository.Login(username, password);
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.Register(string, string, bool, string, string, string, string, string, string)"/>
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
            repository.Register(username, password, isAdmin, owner, firstName, lastName, eMail, adress, city);
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.LoadUsers"/>
        /// </summary>
        public void LoadUsers()
        {
            repository.LoadUsers();
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.SaveUsers"/>
        /// </summary>
        public void SaveUsers()
        {
            repository.SaveUsers();
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.RegisterCompany(string, string, string, string, string, string, int, string)"/>
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
            repository.RegisterCompany(name, contact, phone, email, adress, city, postalcode, companytype);
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.SaveCompany"/>
        /// </summary>
        public void SaveCompany()
        {
            repository.SaveCompany();
        }

        /// <summary>
        /// <inheritdoc cref="UserManagementRepository.LoadCompanys"/>
        /// </summary>
        public void LoadCompanys()
        {
            repository.LoadCompanys();
        }
    }
}

