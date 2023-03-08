namespace SampleAPIs.Models
{
    public class EmployeeModel
    {
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? DOJ { get; set; }
        public string? ProfilePicture { get; set; }
        public IFormFile? PicFile { get; set; }
    }
}
