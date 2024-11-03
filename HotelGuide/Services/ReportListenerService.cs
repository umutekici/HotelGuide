using HotelGuide.Interfaces;
using HotelMicroService.DTOs;
using Newtonsoft.Json;
using RabbitMQ.Interfaces;
using ReportMicroService.Enums;
using ReportMicroService.Interfaces;
using ReportMicroService.Models;

namespace HotelMicroService.Services
{
    public class ReportListenerService : BackgroundService
    {

        private readonly IRabbitMQService _rabbitMQService;
        private readonly IServiceProvider _serviceProvider;
        public ReportListenerService(IRabbitMQService rabbitMQService, IServiceProvider serviceProvider)
        {
            _rabbitMQService = rabbitMQService;
            _serviceProvider = serviceProvider;
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
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();
                    var hotelRepository = scope.ServiceProvider.GetRequiredService<IHotelRepository>();

                    var hotels = await hotelRepository.GetByLocationAsync(reportRequest.Location);
                    var hotelPhones = await hotelRepository.GetPhoneCountByLocationAsync(reportRequest.Location);

                    var report = new Report
                    {
                        Location = reportRequest.Location,
                        HotelCount = hotels.Count(),
                        PhoneCount = (int)hotelPhones,
                        RequestedDate = DateTime.UtcNow,
                        Status = (int)ReportStatus.Completed
                    };

                    await reportService.SaveReportAsync(report);
                }
            }
            catch (Exception ex) {
                throw new Exception("An error occurred while generating the report. Please try again later.", ex);
            }
        }
    }
}
