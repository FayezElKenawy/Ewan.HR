using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Domain.Entities.PayRoll
{
    public class PayRollData:AuditData
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string BankName { get; set; }
        public string IbanNumber { get; set; }
        public DateTime WorkDate { get; set; }
        public int MonthDays { get; set; }
        public int DirectAbsent { get; set; }
        public int AbsentWithPermission { get; set; }
        public int AbsentWithouPermission { get; set; }
        public int MedicalAbsent { get; set; }
        public int DelayWithPermission { get; set; }
        public int DelayWithoutPermission { get; set; }
        public int DelayWithoutCutting { get; set; }

    }
}
