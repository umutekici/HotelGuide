using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Interfaces;
using ReportMicroService.DTOs;
using ReportMicroService.Interfaces;
using ReportMicroService.Models;

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
        public async Task<IActionResult> RequestReport([FromBody] ReportRequest reportRequest)
        {
            try
            {
                _rabbitMQService.Publish(reportRequest, "reportQueue"); 
                return Accepted(new { message = "Report request accepted" });
            } 
            catch (Exception ex)
            {
                throw new Exception("Error while waiting for response: " + ex.Message);
            }
        
        }
    }
}
