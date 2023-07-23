namespace SharedCoreLibrary.Application.Services
{
    public interface IApiHelper
    {
        //ReturnModel Post<ReturnModel>(string envUrl, string endpointUrl, string requestBody);
        Task<TModel> Get<TModel>(string baseUrl, string apiUrl);
        Task<List<TModel>> GetList<TModel>(string baseUrl, string apiUrl);
        Task<TModel> Post<TModel>(string baseUrl, string apiUrl, object postedData, bool AddCustomHeader = false,
            string CustomHeaderName = "", string CustomHeaderValue = "");
        Task<object> Put<TModel>(string baseUrl, string apiUrl, TModel postedData);
        Task<object> Delete<TModel>(string baseUrl, string apiUrl);
    }
}
