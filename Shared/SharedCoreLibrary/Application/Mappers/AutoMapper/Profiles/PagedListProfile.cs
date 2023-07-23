using AutoMapper;
using SharedCoreLibrary.Domain.Entities;

namespace SharedCoreLibrary.Application.Mappers.AutoMapper.Profiles
{
    internal class PagedListProfile : Profile
    {
        public PagedListProfile()
        {
            CreateMap(typeof(PagedList<>), typeof(PagedList<>));
        }
    }
}
