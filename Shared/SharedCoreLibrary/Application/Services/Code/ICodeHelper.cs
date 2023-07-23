
namespace SharedCoreLibrary.Application.Services
{
    public interface ICodeHelper
    {
        string NewDN(string table_name, int totalNoDigits = 3, string prefix = "", string keyName = "Id");
        string NewYYMMDN(string table_name, DateTime date, int noDigits = 4, string keyName = "Id");
    }
}
