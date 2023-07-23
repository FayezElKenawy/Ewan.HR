namespace Ewan.Finance.API.Common.MiddleWares
{
    public class HeaderMiddleWare
    {
        private readonly RequestDelegate _next;
        public HeaderMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && !authorizationHeader.StartsWith("Bearer "))
                httpContext.Request.Headers["Authorization"] = "Bearer " + authorizationHeader;

            await _next(httpContext); // calling next middleware
        }
    }
}
