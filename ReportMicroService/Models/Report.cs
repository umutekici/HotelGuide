namespace ReportMicroService.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int PhoneCount { get; set; }
        public int Status { get; set; }
    }
}
