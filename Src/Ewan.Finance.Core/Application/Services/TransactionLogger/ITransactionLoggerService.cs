using Ewan.Finance.Core.Application.Models.TransactionLogger;

namespace Ewan.Finance.Core.Application.Services
{
    public interface ITransactionLoggerService
    {
        void Add(TransactionLoggerModel transactionLoggerModel);
        List<TransactionLoggerModel> GetListPage(string sectorType, string module, string entityName, int pageNumber = 0, int pageSize = 10);
    }
}
