namespace Ewan.HR.Core.Application.Models
{
    public class EmployeeRequestVM
    {
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public string DirectManagerId { get; set; }
        public string DirectManagerName { get; set; }
    }
}
