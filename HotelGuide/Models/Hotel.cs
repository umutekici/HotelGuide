using HotelMicroService.Models;

namespace HotelGuide.Model
{
    public class Hotel
    {
        public Guid UUID { get; set; }
        public string Name { get; set; }
        public List<Authority> Authorities { get; set; } = new List<Authority>();
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
