using Newtonsoft.Json;

namespace UserLibrary
{
    internal class UserManagementRepository : IUserManagementRepository
    {

        private List<User> AccUserList = new List<User>();
        private const string filePathUser = "users.json";
        private List<Company> CompanyList = new List<Company>();
        private const string filePathCompany = "companys.json";



        public UserManagementRepository() 
        {
            LoadUsers();
            LoadCompanys();
        }

        public bool Login(string username, string password)
        {
            var currentUser = AccUserList.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            if (currentUser != null)
            {
                return true;
            }
            return false;
        }

        public void Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city)
        {
            AccUserList.Add(new User(username, password, isAdmin, owner, firstName, lastName, eMail, adress, city));
            SaveUsers();
        }

        public void RegisterCompany(string name, string contact, string phone, string email, string adress, string city, int postalcode, string companytype)
        {
            CompanyList.Add(new Company(name, contact, phone, email, adress, city, postalcode, companytype));
            SaveCompany();
        }

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

        public void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(AccUserList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePathUser, json);
        }

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

        public void SaveCompany()
        {
            string json = JsonConvert.SerializeObject(CompanyList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePathCompany, json);
        }
    }
}
