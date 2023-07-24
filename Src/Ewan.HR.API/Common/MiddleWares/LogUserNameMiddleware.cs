using Serilog.Context;

namespace Ewan.HR.API.Common.MiddleWares
{
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LogUserNameMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            this.next = next;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task Invoke(HttpContext context)
        {

            LogContext.PushProperty("UserName", GetCurrentUserNameAr());

            return next(context);
        }

        private string GetCurrentUserNameAr()
        {
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type.ToLower() == "namear").Value.ToString();
            }

            return null;
        }
    }
}
