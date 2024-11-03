using HotelGuide.Context;
using HotelGuide.Interfaces;
using HotelGuide.Model;
using HotelMicroService.Enums;
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

        #region Contact Operations

        public async Task<ContactInfo> GetContactByIdAsync(Guid id)
        {
            return await _context.ContactInfos.FindAsync(id);
        }

        public async Task CreateContactAsync(ContactInfo contactInfo)
        {
            await _context.ContactInfos.AddAsync(contactInfo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Guid id)
        {
            var contact = await GetContactByIdAsync(id);
            if (contact != null)
            {
                _context.ContactInfos.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        public async Task<IEnumerable<Hotel>> GetByLocationAsync(string location)
        {
            try
            {
                var result = _context.Hotels
                .Where(h => h.ContactInfos.Any(ci =>
                    ci.Type == ContactType.Location &&
                    ci.Content.ToLower() == location.ToLower()))
                .ToList();

                return result;
            }

            catch (Exception ex) {
                throw new Exception("Failed to retrieve hotels by location.", ex);
            }
        }

        public async Task<int> GetPhoneCountByLocationAsync(string location)
        {
            try
            {
                var phoneCount = await _context.Hotels
                    .Where(h => h.ContactInfos.Any(ci =>
                        ci.Type == ContactType.Location &&
                        ci.Content.ToLower() == location.ToLower()))
                    .SelectMany(h => h.ContactInfos
                        .Where(ci => ci.Type == ContactType.Phone))
                    .CountAsync();

                return phoneCount;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve hotel phones by location.", ex);
            }
        }

    }
}
