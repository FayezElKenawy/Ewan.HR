using Microsoft.Extensions.Configuration;
using SharedCoreLibrary.Application.Services;

namespace Ewan.HR.Core.Application.Services.External.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IApiHelper _apiHelper;
        private readonly IConfiguration _config;
        public IdentityService(IApiHelper apiHelper,IConfiguration config)
        {
            _apiHelper = apiHelper;
            _config = config;
        }
        public async Task<bool> IsUserHasPermission(int userId, string permissionName)
        {
            var result = await _apiHelper.Get<bool>(_config["Url:RMS"], @$"/BroaERP.Core.API/Security/Auth/VerifyUserPermission?userId={userId}&permission={permissionName}");
            return await Task.FromResult(result);
        }

        public async Task<bool> IsValidAPIKey(string apikey)
        {
            string apiKey = _config["KEY:APIKEY"];
            if (apiKey == apikey)
                return await Task.FromResult(true);
            else
                return await Task.FromResult(false);
        }
    }
}
