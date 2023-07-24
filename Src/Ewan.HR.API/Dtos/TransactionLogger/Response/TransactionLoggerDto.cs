namespace Ewan.HR.API.Dtos.TransactionLogger.Response
{
    public class TransactionLoggerDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime OperationDate { get; set; }
        public string OperationType { get; set; }
        public string SectorType { get; set; }
        public string Module { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
    }
}
