
namespace SharedCoreLibrary.Application.Services
{
    public interface IDynamicsApiHelper
    {
        ReturnModel Post<ReturnModel>(string envUrl, string endpointUrl, string requestBody);
    }
}
