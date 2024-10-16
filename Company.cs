namespace UserLibrary
{
    public class Company
    {
        #region Private Member
        private Guid guid;
        private string name;
        private string? contact;
        private string? phone;
        private string? email;
        private string? adress;
        private string? city;
        private string? postalcode;
        private string? companytype;
        #endregion

        public Company(string name, string contact, string phone, string email, string adress, string city, string postalcode, string companytype)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Contact = contact;
            Phone = phone;
            Email = email;
            Adress = adress;
            City = city;
            Postalcode = postalcode;
            Companytype = companytype;
        }

        public Guid Guid
        {
            get
            {
                return guid;
            }
            set
            {
                guid = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string? Contact
        {
            get
            {
                return contact;
            }
            set
            {
                contact = value;
            }
        }

        public string? Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
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

        public string? Postalcode
        {
            get
            {
                return postalcode;
            }
            set
            {
                postalcode = value;
            }
        }

        public string? Companytype
        {
            get
            {
                return companytype;
            }
            set
            {
                companytype = value;
            }
        }
    }
}
