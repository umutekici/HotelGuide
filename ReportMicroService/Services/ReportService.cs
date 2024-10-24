using ReportMicroService.Context;
using ReportMicroService.DTOs;
using ReportMicroService.Interfaces;
using ReportMicroService.Models;

namespace ReportMicroService.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportContext _context;

        public ReportService(ReportContext context)
        {
            _context = context;
        }

        public async Task<Report> CreateReportAsync(ReportCreateDto reportDto)
        {
            var report = new Report
            {
                ReportId = Guid.NewGuid(),
                RequestedDate = DateTime.UtcNow,
                Location = reportDto.Location,
                Status = "Hazırlanıyor"
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            // Raporu hazırlamak için arka planda bir iş başlatıyoruz.
            _ = PrepareReportAsync(report); // Task'ı başlatıyoruz, sonucu beklemiyoruz

            return report;
        }

        private async Task PrepareReportAsync(Report report)
        {
            // Burada otel ve iletişim bilgilerini sayabilirsiniz.
            await Task.Delay(5000); // Simülasyon: Rapor hazırlama süresi

            // Raporun bilgilerini güncelleyelim (örnek değerler)
            report.HotelCount = 10; // Örnek otel sayısı
            report.ContactCount = 25; // Örnek iletişim bilgisi sayısı
            report.Status = "Tamamlandı";

            await _context.SaveChangesAsync();
        }

        public List<Report> GetReports()
        {
            return _context.Reports.ToList();
        }

        public Report GetReportById(Guid reportId)
        {
            return _context.Reports.Find(reportId);
        }
    }
}
