using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace UserLibrary
{
    internal class UserService : IUserLibrary
    {
        public User user;

        private List<User> lstUserList = new List<User>();
        private const string filePath = "users.json";
        public User _currentUser;


        public UserService()
        {
            LoadUsers();

        }

        public bool Login(string username, string password)
        {
            _currentUser = lstUserList.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            return _currentUser != null;
        }

        public bool Register(string username, string password)
        {
            if (lstUserList.Any(u => u.UserName == username))
            {
                return false;
            }

            lstUserList.Add(new User(username, password));
            SaveUsers();
            return true;
        }

        public bool SetUserInfo(string firstName, string lastName,  string eMail, string adress, string city, string username, string password) 
        { 
            if (_currentUser == null)
            {
                return false;
            }
            lstUserList.Add(new UserInfo(firstName, lastName, eMail, adress, city, username, password));
            SaveUsers();
            return true;
        }

        public void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                lstUserList = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        public void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(lstUserList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}

