using AutoMapper;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Application.Services.Employee;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig
{
    public class AddRequestMappingConfig : IMappingAction<EmployeeVM, AddRequestVM>
    {
        private readonly IEmployeeService _employeeService;

        public AddRequestMappingConfig(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public void Process(EmployeeVM source, AddRequestVM destination, ResolutionContext context)
        {
            destination.EmployeeName = source.FristName + ' ' + source.LastName;
            destination.EmployeeNumber = source.EmployeeNumber;
            destination.DepartmentId = source.DepartementId;
            destination.Department = _employeeService.FindDepartmentById(source.DepartementId).Title.ToString();
        }
    }
}
