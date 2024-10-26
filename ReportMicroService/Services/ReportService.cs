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

        public async Task<Report> CreateReportAsync(ReportRequest reportRequest)
        {
            Report report = new Report()
            {
                Id = Guid.NewGuid(),
                RequestedDate = DateTime.Now,
                Location = reportRequest.Location
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
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
