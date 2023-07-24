using AutoMapper;
using Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Domain.Entities.Employee;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeData, EmployeeVM>().AfterMap<EmployeeVMMappingConfig>();
            CreateMap<AddEmployeeVm, EmployeeData>().AfterMap((s, d) =>
            {
                d.EmployeeId = s.EmployeeId;
                d.DepartementId = s.Department;
                d.DirectManager = s.DirectManagerId;

            });
            CreateMap<EmployeeData, AddEmployeeVm>().AfterMap<EmployeeMappingConfig>();
            
            CreateMap<EmployeeData, EmployeeRequestVM>();
        }
    }
}
