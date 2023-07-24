using AutoMapper;
using Ewan.HR.Core.Application.Models.Company;
using Ewan.HR.Core.Domain.Entities.Company;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentVM>();
            CreateMap<DepartmentVM, Department>();

        }
    }
}
