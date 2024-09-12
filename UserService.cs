using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace UserLibrary
{
    public class UserService : IUserService
    {
        private IUserManagementRepository repository;

        public UserService()
        {
            repository = new UserManagementRepository();
        }

        public bool Login(string username, string password)
        {
            return repository.Login(username, password);
        }

        public void Register(string username, string password, bool isAdmin, string owner, string firstName, string lastName, string eMail, string adress, string city)
        {
            repository.Register(username, password, isAdmin, owner, firstName, lastName, eMail, adress, city);
        }

        public void LoadUsers()
        {
            repository.LoadUsers();
        }

        public void SaveUsers()
        {
            repository.SaveUsers();
        }

        public void RegisterCompany(string name, string contact, string phone, string email, string adress, string city, int postalcode, string companytype)
        {
            repository.RegisterCompany(name, contact, phone, email, adress, city, postalcode, companytype);
        }

        public void SaveCompany()
        {
            repository.SaveCompany();
        }

        public void LoadCompanys()
        {
            repository.LoadCompanys();
        }
    }
}

