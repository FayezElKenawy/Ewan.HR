using Ewan.HR.Core.Application.Models.Company;
using Microsoft.AspNetCore.Identity;

namespace Ewan.HR.Core.Application.Services.Company
{
    public interface IDpartmentService
    {
        #region Retreving Data
        Task<IList<ShowDepartmentVM>> GetAll();
        Task<ShowDepartmentVM> GetById(object id);
        #endregion

        #region insert,update,delete
        Task<DepartmentVM> Add(DepartmentVM employee);
        //Task<TEntity> Update(object id, TEntity entity);
        IdentityResult Delete(ShowDepartmentVM employee);
        #endregion
    }
}
