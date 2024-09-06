using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;

namespace UserLibrary
{
    internal class UserManagementRepository : IUserManagementRepository
    {

        private List<User> AccUserList = new List<User>();
        private const string filePathUser = "users.json";



        public UserManagementRepository() 
        {
            LoadUsers();
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
    }
}
