using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Ewan.Finance.API.Common.Filters
{
    public class NonProductionAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var env = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
            if (env.IsProduction() || env.IsStaging())
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
