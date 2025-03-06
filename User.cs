namespace DCS.User
{
    public class User
    {
        public User()
        {

        }

        public Guid Guid { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public bool KeepLoggedIn { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get; set; }

        public bool IsLocalUser { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? SubstitutionEnd { get; set; }

        public DateTime? LastManipulation {  get; set; }
    }
}
