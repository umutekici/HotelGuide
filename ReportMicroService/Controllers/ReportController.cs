using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interfaces;
using ReportMicroService.DTOs;
using ReportMicroService.Interfaces;

namespace ReportMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IRabbitMQService _rabbitMQService;

        public ReportController(IReportService reportService, IRabbitMQService rabbitMQService)
        {
            _reportService = reportService;
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportRequest reportRequest)
        {
            var report = await _reportService.CreateReportAsync(reportRequest);
            _rabbitMQService.Publish(reportRequest, "reportQueue");
            return CreatedAtAction(nameof(GetReports), new { id = report.Id }, report);
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
