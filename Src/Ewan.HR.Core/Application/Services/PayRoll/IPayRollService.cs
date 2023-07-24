using Ewan.HR.Core.Application.Models.Global;

namespace Ewan.HR.Core.Application.Services.PayRoll
{
    public class IPayRollService
    {
        public Task<List<IPayRollService>> Add { get; set; }
    }
}
