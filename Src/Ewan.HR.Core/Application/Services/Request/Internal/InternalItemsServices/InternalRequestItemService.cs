using AutoMapper;
using Ewan.HR.Core.Application.Models.Request.InternalRequest;
using Ewan.HR.Core.Domain.Entities.Request.Internal;
using Ewan.HR.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Request.Internal.InternalItemsServices
{
    public class InternalRequestItemService : IInternalRequestItemService
    {
        #region Private Members
        private readonly IHRUnitOfWork _unitofwork;
        private IMapper _mapper;
        #endregion
        #region Constructor
        public InternalRequestItemService(IHRUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        #endregion

        #region Functions
        public async Task<IdentityResult> Add(InternalRequestItemVM internalRequestItemVM)
        {
            var result = _unitofwork
                                        .InternalRequestItemsRepository
                                        .Add(_mapper.Map<InternalRequestItem>(internalRequestItemVM));
            await _unitofwork.CompleteAsync();
            if (result != null)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed();
        }

        public async Task<IList<InternalRequestItemsShowVM>> GetAll()
        {
            return _mapper.Map<IList<InternalRequestItemsShowVM>>(await _unitofwork.InternalRequestItemsRepository.GetListAsync());
        }

        public Task<InternalRequestItemVM> GetById(string id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
