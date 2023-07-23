
using AutoMapper;
using Ewan.HR.Core.Application.Models.Request.InternalRequest;
using Ewan.HR.Core.Domain.Entities.Request.Internal;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class InternalRequestItemProfile : Profile
    {
        public InternalRequestItemProfile()
        {
            //Add
            CreateMap<InternalRequestItemVM, InternalRequestItem>();
            //Get
            CreateMap<InternalRequestItem, InternalRequestItemsShowVM>();
        }
    }
}
