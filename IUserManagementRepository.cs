using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLibrary
{
    internal interface IUserManagementRepository
    {
        bool Login(string username, string password);
        void Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city);
        void LoadUsers();
        void SaveUsers();
    }
}
