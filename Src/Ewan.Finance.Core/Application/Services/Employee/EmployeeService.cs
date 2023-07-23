using AutoMapper;
using Ewan.HR.Core.Application.Models.Company;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Domain.Interfaces;

namespace Ewan.HR.Core.Application.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        #region privateMember
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public virtual Task<IList<EmployeeVM>> Employees
        {
            get
            {
                return Task.FromResult(_mapper.Map<IList<EmployeeVM>>( _unitOfWork.EmployeeRepository.GetListAsync().Result));
            }
        }
        #endregion

        #region Constructor
        public EmployeeService(IHRUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<AddEmployeeVm> Add( string id)
        {
            throw new NotImplementedException();
        }

        public AddEmployeeVm Find(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AddEmployeeVm> Update(string id, AddEmployeeVm employee)
        {
            throw new NotImplementedException();
        }

        public IList<DepartmentVM> AllDepartments()
        {
            throw new NotImplementedException();
        }

        public DepartmentVM FindDepartmentById(string id)
        {
            throw new NotImplementedException();
        }

        public string UpdateDepartment(DepartmentVM department)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeVM> SelectCustom(string id)
        {
            throw new NotImplementedException();
        }

        public AddEmployeeVm FindByEmployeeNumber(string EmployeeNumber)
        {
            throw new NotImplementedException();
        }

        public string ReturnEmployeeId(string EmployeeNumber)
        {
            throw new NotImplementedException();
        }

        public string ReturnEmployeeNumber(string EmployeeId)
        {
            throw new NotImplementedException();
        }

        public string ReturnEmployeeName(string EmployeeId)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeVM> RetuernDirectmanager(string EmplyeeId)
        {
            throw new NotImplementedException();
        }

        public EmployeeVM ReturnCeo()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implmented
        //public async Task<AddEmployeeVm> Add(AddUserVM user, string id)
        //{
        //    var employee = _mapper.Map<AddEmployeeVm>(user);
        //    var department = _unitOfWork.Departments.GetOne(d => d.DepartmentId == user.Department).Result;
        //    employee.DirectManagerId = department.ManagerId;
        //    employee.EmployeeId = id;
        //    var result = await _unitOfWork.Employees.Add(_mapper.Map<Employee>(employee));

        //    if (user.Position == Positions.Manager.ToString() && result != null && department.ManagerId != null)
        //    {
        //        department.ManagerId = user.EmployeeNumber;
        //        await _unitOfWork.Departments.CustomUpdate(department);
        //    }
        //    await _unitOfWork.Complete();
        //    return employee;
        //}

        //public IList<DepartmentVM> AllDepartments()
        //{
        //    return _mapper.Map<IList<DepartmentVM>>(_unitOfWork.Departments.GetAll().Result);
        //}

        //public AddEmployeeVm Find(string id)
        //{
        //    var h = _mapper.Map<AddEmployeeVm>(_unitOfWork.Employees.GetOne(e => e.EmployeeId == id || e.EmployeeNumber == id).Result);
        //    if (h != null)
        //        return h;
        //    else
        //        return _mapper.Map<AddEmployeeVm>(_userService.Find(id));
        //}
        //public AddEmployeeVm FindByEmployeeNumber(string EmployeeNumber)
        //{
        //    var h = _mapper.Map<AddEmployeeVm>(_unitOfWork.Employees.GetOne(e => e.EmployeeNumber == EmployeeNumber).Result);
        //    if (h != null)
        //        return h;
        //    else
        //        return new AddEmployeeVm();
        //}
        //public DepartmentVM FindDepartmentById(string id)
        //{
        //    try
        //    {
        //        var dpt = _mapper.Map<DepartmentVM>(_unitOfWork.Departments.GetById(id).Result);
        //        return dpt == null ? new DepartmentVM() { DepartmentId = "", ManagerId = "", Title = "" } : dpt;
        //    }
        //    catch (Exception)
        //    {

        //        return new DepartmentVM();
        //    }

        //}

        //public async Task<AddEmployeeVm> Update(string id, AddEmployeeVm entity)
        //{
        //    var h = _mapper.Map<AddEmployeeVm>(await _unitOfWork.Employees.GetOne(e => e.EmployeeId == id));
        //    if (h == null)
        //    {
        //        await _unitOfWork.Employees.Add(_mapper.Map<Employee>(entity));
        //    }
        //    else
        //    {
        //        if (h.Photo == null)
        //        {
        //            await _unitOfWork.Employees.Update(id, _mapper.Map<Employee>(entity));
        //        }
        //        else
        //        {
        //            entity.Photo = h.Photo;
        //            await _unitOfWork.Employees.Update(id, _mapper.Map<Employee>(entity));
        //        }

        //    }
        //    await _unitOfWork.Complete();
        //    return entity;
        //}

        //public string UpdateDepartment(DepartmentVM department)
        //{
        //    var result = _unitOfWork.Departments.CustomUpdate(_mapper.Map<Department>(department)).Result;
        //    _unitOfWork.Complete();
        //    return result;
        //}

        //public async Task<EmployeeVM> SelectCustom(string id)
        //{
        //    try
        //    {
        //        var x = await _unitOfWork.Employees.GetOne(c => c.EmployeeId == id || c.EmployeeNumber == id);
        //        return _mapper.Map<EmployeeVM>(x);
        //    }
        //    catch (Exception)
        //    {

        //        return new EmployeeVM() { };
        //    }

        //}

        //public string ReturnEmployeeId(string EmployeeNumber)
        //{
        //    return Find(EmployeeNumber).EmployeeId;
        //}

        //public string ReturnEmployeeNumber(string EmployeeId)
        //{
        //    var r = SelectCustom(EmployeeId).Result;
        //    return r.EmployeeNumber.ToString();
        //}
        //public string ReturnEmployeeName(string Id)
        //{
        //    var emp = SelectCustom(Id).Result;
        //    return emp.FristName + ' ' + emp.LastName;
        //}

        //public async Task<EmployeeVM> RetuernDirectmanager(string EmplyeeId)
        //{
        //    return await SelectCustom(SelectCustom(EmplyeeId).Result.DirectManagerId);
        //}

        //public EmployeeVM ReturnCeo()
        //{
        //    var t = Employees.Result.FirstOrDefault(e => e.Position == "CEO");
        //    return t;
        //}

        #endregion
    }
}
