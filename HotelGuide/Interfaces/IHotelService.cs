using HotelGuide.Model;

namespace HotelGuide.Interfaces
{
    public interface IHotelService
    {
        Task CreateHotel(Hotel hotel);
        Task DeleteHotel(Guid uuid);
        Task<List<Hotel>> GetAllHotels();
        Task<Hotel> GetHotelById(Guid uuid);
    }
}
