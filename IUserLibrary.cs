using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLibrary
{
    public interface IUserLibrary
    {
        bool Login(string username, string password);
        bool Register(string username, string password);
        bool SetUserInfo(string firstName, string lastName, string eMail, string adress, string city, string username, string password);
        void LoadUsers();
        void SaveUsers();
        User GetCurrentUser();
        void SetCurrentUser(User user);
    }
}
