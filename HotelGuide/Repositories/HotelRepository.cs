using HotelGuide.Context;
using HotelGuide.Interfaces;
using HotelGuide.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelGuide.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelContext _context;

        public HotelRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<Hotel> GetHotelByIdAsync(Guid uuid)
        {
            return await _context.Hotels.FindAsync(uuid);
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task CreateHotelAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(Guid uuid)
        {
            var hotel = await GetHotelByIdAsync(uuid);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
