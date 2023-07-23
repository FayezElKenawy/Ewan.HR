using AutoMapper;
using SharedCoreLibrary.Application.Dtos.Response;
using SharedCoreLibrary.Application.Models.Response.SelectList;

namespace SharedCoreLibrary.Application.Mappers.AutoMapper
{
    public class SelectListProfile : Profile
    {
        public SelectListProfile()
        {
            CreateMap<GetSelectListModel, GetSelectListDto>()
                  .ForMember(dest => dest.FullName, opt => opt.MapFrom(
                    s => s.Code +"-"+ s.Name)).ReverseMap(); ;
            CreateMap(typeof(GetSelectListModel<>), typeof(GetSelectListDto<>)).ReverseMap();
        }
    }
}
