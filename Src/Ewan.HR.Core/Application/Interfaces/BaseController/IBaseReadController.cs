using Microsoft.AspNetCore.Mvc;

namespace Ewan.Finance.Core.Application.Interfaces.BaseController
{
    public interface IBaseReadController<TEntity, TSearchModel> : IBaseController
        where TEntity : class
        where TSearchModel : class
    {
        IActionResult Get(int id);
        IActionResult GetPagedList(TSearchModel searchModel);
    }

    public interface IBaseReadController<TEntity, TSearchModel, TService>: IBaseController<TService>
        where TEntity: class
        where TSearchModel: class
        where TService: class
    {
        IActionResult Get(int id);
        IActionResult GetPagedList(TSearchModel searchModel);
    }
}
