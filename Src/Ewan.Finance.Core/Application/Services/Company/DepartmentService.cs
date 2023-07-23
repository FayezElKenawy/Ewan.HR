using AutoMapper;
using Ewan.HR.Core.Application.Models.Company;
using Ewan.HR.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Company
{
    public class DepartmentService : IDpartmentService
    {
        #region Private Fields
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region constructor
        public DepartmentService(IHRUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<DepartmentVM> Add(DepartmentVM employee)
        {
            throw new NotImplementedException();
        }

        public IdentityResult Delete(ShowDepartmentVM employee)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ShowDepartmentVM>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ShowDepartmentVM> GetById(object id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implemented Functions
        //public async Task<DepartmentVM> Add(DepartmentVM entity)
        //{
        //    return _mapper.Map<DepartmentVM>(await _unitOfWork.Departments.Add(_mapper.Map<Department>(entity)));
        //}

        //public IdentityResult Delete(ShowDepartmentVM entity)
        //{
        //    return _unitOfWork.Departments.Delete(_mapper.Map<Department>(entity));
        //}

        //public async Task<IList<ShowDepartmentVM>> GetAll()
        //{
        //    return _mapper.Map<IList<ShowDepartmentVM>>(await _unitOfWork.Departments.GetAll());
        //}

        //public async Task<ShowDepartmentVM> GetById(object id)
        //{
        //    return _mapper.Map<ShowDepartmentVM>(await _unitOfWork.Departments.GetById(id));
        //}
        #endregion
    }
}
