using HotelGuide.Interfaces;
using HotelGuide.Model;
using HotelMicroService.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interfaces;

namespace HotelGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRabbitMQService _rabbitMQService;

        public HotelController(IHotelService hotelService, IRabbitMQService rabbitMQService)
        {
            _hotelService = hotelService;
            _rabbitMQService = rabbitMQService;
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

        [HttpGet("{id}/authorities")]
        public async Task<ActionResult<IEnumerable<Authority>>> GetAuthorities(Guid id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel == null) return NotFound();
            return hotel.Authorities;
        }

        #region Contact Operations

        [HttpPost("{id}/contacts")]
        public async Task<IActionResult> AddContact(Guid id, ContactInfo contact)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel == null) return NotFound();
            contact.Id = Guid.NewGuid();
            await _hotelService.CreateContact(contact);
            return CreatedAtAction(nameof(GetHotel), new { id }, hotel);
        }

        [HttpDelete("{hotelId}/contacts/{contactId}")]
        public async Task<IActionResult> RemoveContact(Guid hotelId, Guid contactId)
        {
            var hotel = await _hotelService.GetHotelById(hotelId);
            if (hotel == null) return NotFound();

            var contact = hotel.ContactInfos.FirstOrDefault(c => c.Id == contactId);
            if (contact == null) return NotFound();

            await _hotelService.DeleteContact(contact.Id);
            return NoContent();
        }

        #endregion

    }
}
