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

        [HttpPost("request-report")]
        public IActionResult RequestReport([FromBody] ReportRequest reportRequest)
        {
            _rabbitMQService.Publish(reportRequest, "reportQueue");
            return Accepted();
        }
    }
}
