using Ewan.HR.Core.Application.Models.Request.InternalRequest;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Request.Internal.InternalItemsServices
{
    public interface IInternalRequestItemService
    {
        Task<IList<InternalRequestItemsShowVM>> GetAll();
        Task<InternalRequestItemVM> GetById(string id);
        Task<IdentityResult> Add(InternalRequestItemVM internalRequestItemVM);

    }
}
