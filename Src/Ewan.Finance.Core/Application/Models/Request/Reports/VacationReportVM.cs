namespace Ewan.HR.Core.Application.Models.Request.Reports
{
    public class VacationReportVM
    {
        public string VacatonNo { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ResumeDate { get; set; }
        public string TotalDayes { get; set; }
    }
}
