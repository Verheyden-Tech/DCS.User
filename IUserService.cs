using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLibrary
{
    public interface IUserService
    {
        bool Login(string username, string password);
        void Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city);
        void LoadUsers();
        void SaveUsers();
        void RegisterCompany(string name, string contact, string phone, string email, string adress, string city, int postalcode, string companytype);
        void SaveCompany();
        void LoadCompanys();
    }
}
