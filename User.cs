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

        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get; set; }

        public string ContactDataFilePath { get; set; }

        public string PhysicalAdressDataFilePath { get; set; }

        public string EmailAdressDataFilePath { get; set; }

        public string PhoneNumberDataFilePath { get; set; }

        public string CompanyDataFilePath { get; set; }

        public string SaveFolderFilePath { get; set; }
    }
}
