namespace Ewan.HR.Core.Application.Services.External.Identity
{
    public interface IIdentityService
    {
        public Task<bool> IsUserHasPermission(int userId, string permissionName);
        public Task<bool> IsValidAPIKey(string apikey);
    }
}
