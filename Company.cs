namespace UserLibrary
{
    public class Company
    {
        private string name;
        private string? phone;
        private string? email;
        private string? adress;
        private string? city;
        private int? postalcode;
        private string? companytype;

        public Company(string name, string phone, string email, string adress, string city, int postalcode, string companytype)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Adress = adress;
            City = city;
            Postalcode = postalcode;
            Companytype = companytype;
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

        public int? Postalcode
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
