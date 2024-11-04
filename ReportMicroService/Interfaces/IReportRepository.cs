using ReportMicroService.Models;

namespace ReportMicroService.Interfaces
{
    public interface IReportRepository
    {
        List<Report> GetReports();
        Report GetReportById(Guid reportId);
        Task<Report> SaveReportAsync(Report report);
    }
}
