using ReportMicroService.Interfaces;
using ReportMicroService.Models;

namespace ReportMicroService.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<Report> GetReports()
        {
            return _reportRepository.GetReports();
        }

        public Report GetReportById(Guid reportId)
        {
            return _reportRepository.GetReportById(reportId);
        }

        public async Task<Report> SaveReportAsync(Report report)
        {
            return await _reportRepository.SaveReportAsync(report);
        }
    }
}
