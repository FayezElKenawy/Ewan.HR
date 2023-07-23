using AutoMapper;
using SharedCoreLibrary.Application.Dtos.Request;
using SharedCoreLibrary.Application.Models.Request.DynamicSearch;
using SharedCoreLibrary.Application.Models.Request;

namespace SharedCoreLibrary.Application.Mappers.AutoMapper.Profiles
{
    public class DynamicSearchProfile : Profile
    {
        public DynamicSearchProfile()
        {
            CreateMap<SearchDto, SearchModel>();
            CreateMap<SearchFieldDto, SearchFieldModel>();
        }
    }
}
