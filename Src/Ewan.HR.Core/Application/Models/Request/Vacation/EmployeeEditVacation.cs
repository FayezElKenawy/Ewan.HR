namespace Ewan.HR.Core.Application.Models.Request.Vacation
{
    public class EmployeeEditVacation
    {
        public string RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ResumptionDate { get; set; }
        public int TotalDayes { get; set; }
    }
}
