using HotelGuide.Interfaces;
using HotelGuide.Model;

namespace HotelGuide.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task CreateHotel(Hotel hotel)
        {
            await _hotelRepository.CreateHotelAsync(hotel);
        }

        public async Task DeleteHotel(Guid uuid)
        {
            await _hotelRepository.DeleteHotelAsync(uuid);
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            return await _hotelRepository.GetAllHotelsAsync();
        }

        public async Task<Hotel> GetHotelById(Guid uuid)
        {
            return await _hotelRepository.GetHotelByIdAsync(uuid);
        }
    }
}
