using ReportMicroService.Context;
using ReportMicroService.Interfaces;
using ReportMicroService.Models;

namespace ReportMicroService.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportContext _context;

        public ReportRepository(ReportContext context)
        {
            _context = context;
        }

        public List<Report> GetReports()
        {
            return _context.Reports.ToList();
        }

        public Report GetReportById(Guid reportId)
        {
            return _context.Reports.Find(reportId);
        }

        public async Task<Report> SaveReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}