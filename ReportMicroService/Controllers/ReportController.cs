using Microsoft.AspNetCore.Mvc;
using ReportMicroService.DTOs;
using ReportMicroService.Interfaces;

namespace ReportMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportCreateDto reportDto)
        {
            var report = await _reportService.CreateReportAsync(reportDto);
            return CreatedAtAction(nameof(GetReportById), new { reportId = report.ReportId }, report);
        }

        [HttpGet]
        public IActionResult GetReports()
        {
            var reports = _reportService.GetReports();
            return Ok(reports);
        }

        [HttpGet("{reportId}")]
        public IActionResult GetReportById(Guid reportId)
        {
            var report = _reportService.GetReportById(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }
}
