using Ewan.HR.Core.Application.Models.Request.InternalRequest;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Request.Internal.InternalRequestServices
{
    public interface IInternalRequestService
    {
        Task<IdentityResult> Add(AddInternalRequestVM addInternalRequest);
        Task<InternalRequestShowVM> GetById(string id);
    }
}
