using HotelGuide.Interfaces;
using HotelGuide.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            hotel.UUID = Guid.NewGuid();
            await _hotelService.CreateHotel(hotel);
            return CreatedAtAction(nameof(GetHotel), new { uuid = hotel.UUID }, hotel);
        }

        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteHotel(Guid uuid)
        {
            await _hotelService.DeleteHotel(uuid);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        [HttpGet("{uuid}")]
        public async Task<IActionResult> GetHotel(Guid uuid)
        {
            var hotel = await _hotelService.GetAllHotels();
            var foundHotel = hotel.FirstOrDefault(h => h.UUID == uuid);
            if (foundHotel == null) return NotFound();
            return Ok(foundHotel);
        }

    }
}
