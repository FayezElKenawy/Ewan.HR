using AutoMapper;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Domain.Entities.Request.Internal;
using Ewan.HR.Core.Application.Models.Request.InternalRequest;
using Ewan.HR.Core.Application.Services.Request.MasterData;
using Ewan.HR.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Ewan.HR.Core.Application.Services.Request.Internal.InternalItemsServices;

namespace Ewan.HR.Core.Application.Services.Request.Internal.InternalRequestServices
{

    public class InternalRequestService : IInternalRequestService
    {
        #region Private Members
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;
        private readonly IInternalRequestItemService _internalItemService;
        #endregion

        #region Constructor
        public InternalRequestService(IHRUnitOfWork unitOfWork, IMapper mapper, IRequestService requestService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestService = requestService;
        }
        #endregion

        #region Functions
        public async Task<IdentityResult> Add(AddInternalRequestVM addInternalRequest)
        {
            try
            {

                var id = _requestService.Add(_mapper.Map<AddRequestVM>(addInternalRequest)).Result;

                foreach (var item in addInternalRequest.RequestItems)
                {
                    if (item.IsSelected == true)
                    {
                        var t = _mapper.Map<InternalRequest>(item);
                        t.RequestId = id;
                        await _unitOfWork.InternalRequestRepository.AddAsync(t);
                    }
                }
                await _unitOfWork.CompleteAsync();
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<InternalRequestShowVM> GetById(string id)
        {
            var requestInfo = _requestService.GetById(id);
            var requestItems = _internalItemService.GetById(id);

            return _mapper.Map<InternalRequestShowVM>(await _unitOfWork.InternalRequestRepository.GetAsync(c => c.Id == id));
        }
        #endregion
    }
}

