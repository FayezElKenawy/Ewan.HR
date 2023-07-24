using AutoMapper;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Application.Services.Employee;
using Ewan.HR.Core.Domain.Entities.Employee;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig
{
    public class EmployeeVMMappingConfig : IMappingAction<EmployeeData, EmployeeVM>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeVMMappingConfig(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public void Process(EmployeeData source, EmployeeVM destination, ResolutionContext context)
        {
            destination.DepartementId = source.DepartementId;
            destination.DepartmentTitle = _employeeService.FindDepartmentById(source.DepartementId).Title;
            destination.DirectManagerId = source.DirectManager;
        }
    }
}
