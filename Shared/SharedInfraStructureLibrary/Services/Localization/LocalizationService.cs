using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using SharedCoreLibrary.Application.Services;
using System.Globalization;

namespace SharedInfraStructureLibrary.Services
{
    public class LocalizationService : ILocalizationService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        
        public LocalizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentCultureName => 
            _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
        public string GetCurrentUICultureName =>
            _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
        public CultureInfo GetCurrentCulture =>
            _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
        public CultureInfo GetCurrentUICulture =>
            _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
    }
}
