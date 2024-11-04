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

        public async Task CreateContact(Hotel hotel)
        {
            await _hotelRepository.CreateHotelAsync(hotel);
        }

        public async Task CreateContact(ContactInfo contactInfo)
        {
            await _hotelRepository.CreateContactAsync(contactInfo);
        }

        public async Task DeleteContact(Guid id)
        {
            await _hotelRepository.DeleteContactAsync(id);
        }

        public async Task<ContactInfo> GetContactById(Guid id)
        {
            return await _hotelRepository.GetContactByIdAsync(id);
        }
    }
}
