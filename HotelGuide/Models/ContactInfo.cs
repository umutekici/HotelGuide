namespace HotelGuide.Model
{
    public class ContactInfo
    {
        public Guid Id { get; set; }
        public Guid HotelUUID { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
