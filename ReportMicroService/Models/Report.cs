namespace ReportMicroService.Models
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int ContactCount { get; set; }
        public string Status { get; set; }
    }
}
