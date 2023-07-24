using Ewan.HR.Core.Application.Services.External.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Ewan.HR.API.Common.Policy
{
    public class UserPolicyRequirement : IAuthorizationRequirement
    {
        private readonly IConfiguration _configuration;

        public UserPolicyRequirement(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> Pass(
            IIdentityService identityService,
            IHttpContextAccessor httpContextAccessor,
            string controller,
            string action)
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return await Task.FromResult(false);

            if(action.ToLower().Contains("selectlist"))
                return await Task.FromResult(true);

            if (!string.IsNullOrEmpty(httpContextAccessor.HttpContext.Request.Headers["ApiKey"]) &&
                 httpContextAccessor.HttpContext.Request.Headers["ApiKey"].FirstOrDefault().ToString() == _configuration["Key:APIKEY"])
                    return await Task.FromResult(true);

            string httpMethod = httpContextAccessor.HttpContext.Request.Method.ToLower();
            string userId = httpContextAccessor.HttpContext.User.Claims
                            .FirstOrDefault(c => c.Type.ToLower().Contains("userid")).Value;

            string permissionFullName = "HR-" + controller + "-" + action;

            return await identityService.IsUserHasPermission(int.Parse(userId), permissionFullName);
        }
    }
}
