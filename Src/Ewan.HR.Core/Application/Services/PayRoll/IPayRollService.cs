using Ewan.HR.Core.Application.Models.PayRoll;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Application.Services.PayRoll
{
    public interface IPayRollService
    {
        public List<PayRollAddVM> Calculate(string dateFrom);
        public PagedList<PayRollAddVM> GetAll(SearchModel searchModel);
        Task<MemoryStream> PayRollDownload(string month);
    }
}
