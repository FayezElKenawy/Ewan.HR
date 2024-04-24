using AutoMapper;
using Ewan.HR.Core.Application.Models;
using Ewan.HR.Core.Domain.Interfaces;

namespace Ewan.HR.Core.Application.Services
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
                return Task.FromResult(_mapper.Map<IList<EmployeeVM>>(_unitOfWork.EmployeeRepository.GetListAsync().Result));
            }
        }
        #endregion

        public EmployeeService(IHRUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
