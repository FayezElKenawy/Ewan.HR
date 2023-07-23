using System.Globalization;

namespace SharedCoreLibrary.Application.Services
{
    public interface ILocalizationService
    {
        string GetCurrentCultureName { get; }
        string GetCurrentUICultureName { get; }
        CultureInfo GetCurrentCulture { get; }
        CultureInfo GetCurrentUICulture { get; }
    }
}
