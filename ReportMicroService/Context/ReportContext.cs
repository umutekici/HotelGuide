using Microsoft.EntityFrameworkCore;
using ReportMicroService.Models;

namespace ReportMicroService.Context
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
    }
}
