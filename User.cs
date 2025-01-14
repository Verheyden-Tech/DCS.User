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

        public bool IsLoggedIn { get; set; }

        public string ContactDataFilePath { get; set; }

        public string CompanyDataFilePath { get; set; }

        public string SaveFolderFilePath { get; set; }
    }
}
