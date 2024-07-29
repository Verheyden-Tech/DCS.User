namespace UserLibrary
{
    public class User
    {
        public User(string username, string password)
        {
            UserName = username;
            PassWord = password;
        }

        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class UserInfo : User
    {
        public UserInfo(string firstName, string lastName, string eMail, string adress, string city, string username, string password) : base(username, password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = eMail;
            Adress = adress;
            City = city;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
    }
}
