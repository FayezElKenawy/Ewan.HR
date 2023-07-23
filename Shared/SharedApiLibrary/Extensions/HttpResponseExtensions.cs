using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace SharedApiLibrary.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddApplicationErrorHeader(this HttpResponse response, string message)
        {
            var stringValues = new StringValues();
            stringValues.Append(message);
            response.Headers.Add("Application-Error", stringValues);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-origin", "*");
        }
    }
}
