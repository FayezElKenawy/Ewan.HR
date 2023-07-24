using AutoMapper;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Application.Models.Request.InternalRequest;
using Ewan.HR.Core.Domain.Entities.Request.Internal;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class InternalRequestProfile : Profile
    {
        public InternalRequestProfile()
        {

            #region InnerRequest
            CreateMap<AddInternalRequestVM, InternalRequest>();
            CreateMap<AddRequestVM, AddInternalRequestVM>();
            CreateMap<InternalRequestItemsShowVM, InternalRequest>().AfterMap((s, d) =>
            {
                d.ItemId = s.Id;
                d.Id = Guid.NewGuid().ToString();
            });
            CreateMap<InternalRequest, InternalRequestShowVM>();
            #endregion
        }
    }
}
