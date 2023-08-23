using AutoMapper;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Application.Services.Employee;
using Ewan.HR.Core.Domain.Entities.Employee;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig
{
    internal class EmployeeMappingConfig : IMappingAction<EmployeeData, AddEmployeeVm>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeMappingConfig(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public void Process(EmployeeData source, AddEmployeeVm destination, ResolutionContext context)
        {
            try
            {
                destination.Location = source.LocationId;
                destination.Department = _employeeService.FindDepartmentById(source.DepartementId).Title.ToString() == null ? "" : _employeeService.FindDepartmentById(source.DepartementId).Title.ToString();
            }
            catch (Exception)
            {

                destination.Department = "";
            }

        }
    }
}
