using AutoMapper;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Application.Services.Employee;
using Ewan.HR.Core.Domain.Entities.Employee;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig
{
    public class EmployeeRequestMappingConfig : IMappingAction<Employee, EmployeeRequestVM>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeRequestMappingConfig(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public void Process(Employee source, EmployeeRequestVM destination, ResolutionContext context)
        {
            destination.EmployeeName = source.FristName + ' ' + source.LastName;
            destination.EmployeeNumber = source.EmployeeNumber;
            destination.Department = _employeeService.Employees.Result.Where(c => c.DepartementId == source.DepartementId).FirstOrDefault().DepartmentTitle.ToString();
           // destination.DirectManagerName = _employeeService.RetuernDirectmanager(source.EmployeeId).Result.FristName + ' '
               // + _employeeService.RetuernDirectmanager(source.EmployeeId).Result.LastName;
        }
    }
}
