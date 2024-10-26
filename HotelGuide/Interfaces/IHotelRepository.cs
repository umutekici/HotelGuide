using HotelGuide.Model;

namespace HotelGuide.Interfaces
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelByIdAsync(Guid uuid);
        Task<List<Hotel>> GetAllHotelsAsync();
        Task CreateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(Guid uuid);
        Task<IEnumerable<Hotel>> GetByLocationAsync(string location);
    }
}
