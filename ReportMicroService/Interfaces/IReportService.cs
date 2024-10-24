using ReportMicroService.DTOs;
using ReportMicroService.Models;

namespace ReportMicroService.Interfaces
{
    public interface IReportService
    {
        Task<Report> CreateReportAsync(ReportCreateDto reportDto);
        List<Report> GetReports();
        Report GetReportById(Guid reportId);
    }
}
