namespace Ewan.HR.Core.Application.Models.Request.Vacation
{
    public class AddVacationVm : AddRequestVM
    {
        public string VacationTypeId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public DateTime ResumptionDate { get; set; } = DateTime.Now.Date;
        public int TotalDayes { get; set; } = 0;
        public string VacationReason { get; set; }
        public int Balance { get; set; } = 0;
        public int VacationDays { get; set; } = 0;
        public int RemainingBalance { get; set; } = 0;
        public string ReplacingEmployeeNumber { get; set; }
        public string ReplacingEmployeeName { get; set; }
        public byte ReplacingSignature { get; set; }
        public string WorkHandover { get; set; }
        public string JobTitle { get; set; }
        public string InOutVisaFor { get; set; }
        public string TicketsFor { get; set; }
        public string TicketsVisaCost { get; set; }
        public DateTime LastReturnDate { get; set; } = DateTime.Now;
        public int PaidDays { get; set; }
        public int UnpaidDays { get; set; }
        public int Holidays { get; set; }
    }
}
