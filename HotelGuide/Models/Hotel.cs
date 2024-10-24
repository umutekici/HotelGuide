namespace HotelGuide.Model
{
    public class Hotel
    {
        public Guid UUID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string AuthorityFirstName { get; set; }
        public string AuthorityLastName { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
