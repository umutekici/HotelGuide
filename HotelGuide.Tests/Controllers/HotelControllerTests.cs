using HotelGuide.Controllers;
using HotelGuide.Interfaces;
using HotelGuide.Model;
using HotelMicroService.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQ.Interfaces;
using Xunit;

public class HotelControllerTests
{
    private readonly Mock<IHotelService> _hotelServiceMock;
    private readonly Mock<IRabbitMQService> _rabbitMQServiceMock;
    private readonly HotelController _controller;

    public HotelControllerTests()
    {
        _hotelServiceMock = new Mock<IHotelService>();
        _rabbitMQServiceMock = new Mock<IRabbitMQService>();
        _controller = new HotelController(_hotelServiceMock.Object, _rabbitMQServiceMock.Object);
    }

    [Fact]
    public async Task CreateHotel_ReturnsCreatedHotel()
    {
        var hotel = new Hotel { Name = "Test Hotel" };
        _hotelServiceMock.Setup(s => s.CreateHotel(It.IsAny<Hotel>())).Returns(Task.CompletedTask);

        var result = await _controller.CreateHotel(hotel);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdHotel = Assert.IsType<Hotel>(createdResult.Value);
        Assert.NotNull(createdHotel.UUID);
        Assert.Equal(hotel.Name, createdHotel.Name);
    }

    [Fact]
    public async Task DeleteHotel_ReturnsNoContent_WhenHotelExists()
    {
        var uuid = Guid.NewGuid();
        _hotelServiceMock.Setup(s => s.DeleteHotel(uuid)).Returns(Task.CompletedTask);

        var result = await _controller.DeleteHotel(uuid);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetAllHotels_ReturnsOkWithHotels()
    {
        var hotels = new List<Hotel>
        {
            new Hotel { UUID = Guid.NewGuid(), Name = "Hotel 1" },
            new Hotel { UUID = Guid.NewGuid(), Name = "Hotel 2" }
        };
        _hotelServiceMock.Setup(s => s.GetAllHotels()).ReturnsAsync(hotels);

        var result = await _controller.GetAllHotels();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedHotels = Assert.IsAssignableFrom<IEnumerable<Hotel>>(okResult.Value);
        Assert.Equal(hotels.Count, returnedHotels.Count());
    }

    [Fact]
    public async Task GetHotel_ReturnsNotFound_WhenHotelDoesNotExist()
    {
        var uuid = Guid.NewGuid();
        var hotels = new List<Hotel>
        {
            new Hotel { UUID = Guid.NewGuid(), Name = "Hotel 1" }
        };
        _hotelServiceMock.Setup(s => s.GetAllHotels()).ReturnsAsync(hotels);

        var result = await _controller.GetHotel(uuid);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetHotel_ReturnsOk_WhenHotelExists()
    {
        var uuid = Guid.NewGuid();
        var hotel = new Hotel { UUID = uuid, Name = "Hotel 1" };
        var hotels = new List<Hotel> { hotel };
        _hotelServiceMock.Setup(s => s.GetAllHotels()).ReturnsAsync(hotels);

        var result = await _controller.GetHotel(uuid);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedHotel = Assert.IsType<Hotel>(okResult.Value);
        Assert.Equal(hotel.UUID, returnedHotel.UUID);
    }

    [Fact]
    public async Task AddContact_ReturnsNotFound_WhenHotelDoesNotExist()
    {
        var uuid = Guid.NewGuid();
        var contact = new ContactInfo { Type = ContactType.Email, Content = "abc@xyz.com" };
        _hotelServiceMock.Setup(s => s.GetHotelById(uuid)).ReturnsAsync((Hotel)null);

        var result = await _controller.AddContact(uuid, contact);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task RemoveContact_ReturnsNoContent_WhenContactExists()
    {
        var hotelId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var hotel = new Hotel
        {
            UUID = hotelId,
            ContactInfos = new List<ContactInfo>
            {
                new ContactInfo { Id = contactId, Type = ContactType.Email, Content = "xyz@example.com" }
            }
        };
        _hotelServiceMock.Setup(s => s.GetHotelById(hotelId)).ReturnsAsync(hotel);
        _hotelServiceMock.Setup(s => s.DeleteContact(contactId)).Returns(Task.CompletedTask);
        
        var result = await _controller.RemoveContact(hotelId, contactId);
      
        Assert.IsType<NoContentResult>(result);
    }
}
