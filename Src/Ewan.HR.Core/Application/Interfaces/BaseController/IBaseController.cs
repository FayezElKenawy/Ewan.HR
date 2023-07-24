namespace Ewan.Finance.Core.Application.Interfaces.BaseController
{
    public interface IBaseController
    {

    }

    public interface IBaseController<TService> : IBaseController
        where TService : class
    {
        TService Service { get; }
    }
}
