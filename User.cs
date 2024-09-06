namespace UserLibrary
{
    public class User
    {
        private string username;
        private string password;
        private bool isAdmin;
        private string? owner;
        private string? firstName;
        private string? lastName;
        private string? email;
        private string? adress;
        private string? city;

        public User(string userName, string passWord, bool isAdmin, string oWner, string firstname, string lastname, string eMail, string aDress, string cIty)
        {
            UserName = userName;
            PassWord = passWord;
            IsAdmin = isAdmin;
            Owner = oWner;
            FirstName = firstname;
            LastName = lastname;
            Email = eMail;
            Adress = aDress;
            City = cIty;
        }

        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
            }
        }

        public string? Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public string? FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string? LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public string? Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string? Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
            }
        }

        public string? City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
    }
}
