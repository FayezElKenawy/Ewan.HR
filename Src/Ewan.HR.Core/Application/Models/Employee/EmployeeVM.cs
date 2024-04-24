namespace Ewan.HR.Core.Application.Models
{
    public class EmployeeVM
    {
        public string EmployeeNumber { get; set; }
        public string NationalId { get; set; }
        public string Position { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DepartementId { get; set; } = "0";
        public string DepartmentTitle { get; set; }
        public string DirectManagerName { get; set; }
        public string DirectManagerId { get; set; }
        public string LocationId { get; set; } = "0";
        public DateTime ResumptionDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; } = "0";
        public string Photo { get; set; } = "yyyy";
        public string EmployeeId { get; set; }
    }
}
