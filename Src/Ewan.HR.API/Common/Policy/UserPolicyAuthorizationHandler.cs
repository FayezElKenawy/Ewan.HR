using Ewan.HR.Core.Application.Services.External.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ewan.HR.API.Common.Policy
{
    public class UserPolicyAuthorizationHandler : AuthorizationHandler<UserPolicyRequirement>
    {
        IIdentityService identityService;
        IHttpContextAccessor contextAccessor;

        public UserPolicyAuthorizationHandler(
            IIdentityService identityService,
            IHttpContextAccessor contextAccessor)
        {
            this.identityService = identityService;
            this.contextAccessor = contextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext authContext,
            UserPolicyRequirement requirement)
        {
            if (authContext.Resource is AuthorizationFilterContext filterContext)
            {
                //var area = (filterContext.RouteData.Values["area"] as string);
                var controller = (filterContext.RouteData.Values["controller"] as string);
                var action = (filterContext.RouteData.Values["action"] as string);

                if (await requirement.Pass(identityService, contextAccessor, controller, action))
                {
                    authContext.Succeed(requirement);
                }
            }

            if (authContext.Resource is DefaultHttpContext httpContext)
            {
                //var area = httpContext.Request.RouteValues["area"].ToString();
                var controller = httpContext.Request.RouteValues["controller"].ToString();
                var action = httpContext.Request.RouteValues["action"].ToString();

                if (await requirement.Pass(identityService, contextAccessor, controller, action))
                {
                    authContext.Succeed(requirement);
                }
            }
        }
    }
}
