using Ewan.HR.Core.Application.Models.Company;
using Ewan.HR.Core.Application.Models.Employee;

namespace Ewan.HR.Core.Application.Services.Employee
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeVM>> Employees { get; }
        Task<AddEmployeeVm> Add( string id);
        AddEmployeeVm Find(string id);
        Task<AddEmployeeVm> Update(string id, AddEmployeeVm employee);
        IList<DepartmentVM> AllDepartments();
        DepartmentVM FindDepartmentById(string id);
        string UpdateDepartment(DepartmentVM department);
        Task<EmployeeVM> SelectCustom(string id);
        AddEmployeeVm FindByEmployeeNumber(string EmployeeNumber);
        string ReturnEmployeeId(string EmployeeNumber);
        string ReturnEmployeeNumber(string EmployeeId);
        string ReturnEmployeeName(string EmployeeId);
        Task<EmployeeVM> RetuernDirectmanager(string EmplyeeId);
        EmployeeVM ReturnCeo();


    }
}
