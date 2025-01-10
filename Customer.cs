namespace UserLibrary
{
    public class Customer
    {
        public Customer() { }

        public Guid Guid { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MailAddress { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public int? PostalCode { get; set; }

        public Company? Company { get; set; }

        public bool? IsActive { get; set; }
    }
}
