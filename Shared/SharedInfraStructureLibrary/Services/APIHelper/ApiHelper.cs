using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Globalization;
using SharedCoreLibrary.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace SharedInfraStructureLibrary.Services
{
    public class ApiHelper: IApiHelper
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiHelper(IConfiguration config,IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TModel> Get<TModel>(string baseUrl, string apiUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
                if(_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value.ToString().Remove(0,6));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));     
          
                HttpResponseMessage res = await client.GetAsync(apiUrl);

                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TModel>(response);
                }
                return default;
            }
        }

        public async Task<List<TModel>> GetList<TModel>(string baseUrl, string apiUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value.ToString().Remove(0, 6));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("User-Agent", _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToList());

                HttpResponseMessage res = await client.GetAsync(apiUrl);
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<TModel>>(response);
                }
                return null;
            }
        }

        public async Task<TModel> Post<TModel>(string baseUrl, string apiUrl, object postedData,bool AddCustomHeader = false,
            string CustomHeaderName = "", string CustomHeaderValue= "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value.ToString().Remove(0, 6));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("User-Agent", _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToList());

                if (AddCustomHeader)
                {
                    client.DefaultRequestHeaders.Add(CustomHeaderName, CustomHeaderValue);
                }

                var result = await client.PostAsJsonAsync(apiUrl, postedData);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<TModel>(response);
                    return data;
                }
                return default;
            }
        }

        public async Task<object> Put<TModel>(string baseUrl, string apiUrl, TModel postedData)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value.ToString().Remove(0, 6));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("User-Agent", _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToList());

                var result = await client.PutAsJsonAsync(apiUrl, postedData);
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        var response = result.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<TModel>(response);
                    }
                    catch (Exception ex)
                    {
                        return default;
                    }

                }
                return null;
            }
        }

        public async Task<object> Delete<TModel>(string baseUrl, string apiUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value.ToString().Remove(0, 6));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("User-Agent", _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToList());

                var result = await client.DeleteAsync(apiUrl);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TModel>(response);
                }
                return null;
            }
        }
    }
}
