using System.Security.Cryptography;

namespace UserLibrary
{
    public class User
    {
        public User()
        {

        }

        public Guid Guid { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public bool IsAdmin { get; set; }

        public string? Owner { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Adress { get; set; }

        public string? City { get; set; }
    }
}
