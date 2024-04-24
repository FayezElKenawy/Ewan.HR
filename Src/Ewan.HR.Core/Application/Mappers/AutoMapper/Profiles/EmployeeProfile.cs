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
            CreateMap<Employee, EmployeeVM>().AfterMap<EmployeeVMMappingConfig>();
            CreateMap<AddEmployeeVm, Employee>().AfterMap((s, d) =>
            {
                d.DepartementId = s.Department;
                d.DirectManager = s.DirectManagerId;

            });
            CreateMap<Employee, AddEmployeeVm>().AfterMap<EmployeeMappingConfig>();
            
            CreateMap<Employee, EmployeeRequestVM>();
        }
    }
}
