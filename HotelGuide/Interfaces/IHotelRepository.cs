using HotelGuide.Model;

namespace HotelGuide.Interfaces
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelByIdAsync(Guid uuid);
        Task<List<Hotel>> GetAllHotelsAsync();
        Task CreateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(Guid uuid);
        Task CreateContactAsync(ContactInfo contactInfo);
        Task DeleteContactAsync(Guid id);
        Task<ContactInfo> GetContactByIdAsync(Guid id);
        Task<IEnumerable<Hotel>> GetByLocationAsync(string location);

    }
}
