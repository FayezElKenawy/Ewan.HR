using Microsoft.AspNetCore.Mvc;

namespace Ewan.Finance.Core.Application.Interfaces.BaseController
{
     public interface IBaseCrudController<TEntity, TSearchModel, TPostEntity> : 
        IBaseReadController<TEntity, TSearchModel>
        where TEntity: class
        where TSearchModel: class
        where TPostEntity: class
    {
        IActionResult Create(TPostEntity postEntity);
        IActionResult Update(TPostEntity postEntity);
        IActionResult Delete(int id);
    }

    public interface IBaseCrudController<TEntity, TSearchModel, TPostEntity, TService> : 
        IBaseReadController<TEntity, TSearchModel, TService>
        where TEntity: class
        where TSearchModel: class
        where TPostEntity: class
        where TService: class
    {
        IActionResult Create(TPostEntity postEntity);
        IActionResult Update(TPostEntity postEntity);
        IActionResult Delete(int id);
    }
}
