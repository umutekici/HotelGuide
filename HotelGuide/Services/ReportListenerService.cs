using HotelGuide.Interfaces;
using HotelMicroService.DTOs;
using Newtonsoft.Json;
using RabbitMQ.Interfaces;
using ReportMicroService.Enums;
using ReportMicroService.Models;

namespace HotelMicroService.Services
{
    public class ReportListenerService : BackgroundService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRabbitMQService _rabbitMQService;

        public ReportListenerService(IRabbitMQService rabbitMQService, IHotelRepository hotelRepository)
        {
            _rabbitMQService = rabbitMQService;
            _hotelRepository = hotelRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMQService.Subscribe("reportQueue", async (message) =>
            {
                var reportRequest = JsonConvert.DeserializeObject<ReportRequest>(message);
                if (reportRequest != null)
                {
                    await HandleReportRequest(reportRequest);
                }
            });

            return Task.CompletedTask;
        }

        private async Task HandleReportRequest(ReportRequest reportRequest)
        {
            var hotels = await _hotelRepository.GetByLocationAsync(reportRequest.Location);
            var report = new Report
            {
                Id = Guid.NewGuid(),
                Location = reportRequest.Location,
                HotelCount = hotels.Count(),
                RequestedDate = DateTime.UtcNow,
                Status = (int)ReportStatus.Completed
            };
        }

    }
}
