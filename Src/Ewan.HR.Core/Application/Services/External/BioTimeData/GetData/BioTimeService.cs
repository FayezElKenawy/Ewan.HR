using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.GetData
{
    public class BioTimeService : IBioTimeService
    {
        public async Task<List<GetAttendanceVM>> GetEmployeeAttendance(string user, string start, string end)
        {
            var startDate = "";
            var endDate   = "";

            if (string.IsNullOrEmpty(start)&&string.IsNullOrEmpty(end))
            {
                startDate = null;
                endDate = null;
            }
            else
            {
                startDate = start.Trim() + " 00:00:00";
                endDate = end.Trim() + " 23:59:00";
            }

            var endPoint = $"iclock/api/transactions/?emp_code={user}&start_time={startDate}&end_time={endDate}&page_size=100000000";
            //var endPoint = "iclock/api/transactions/?emp_code=2425&page_size=1000000";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://10.1.0.10:80/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("jwt", GetToken());
                HttpResponseMessage Res = client.GetAsync(endPoint).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var empAttendance = await Res.Content.ReadAsStringAsync();
                    var attendanceList = JsonConvert.DeserializeObject<GolbalOutSourceDataVM<GetAttendanceVM>>(empAttendance)
                                            .data
                                          .OrderBy(c => c.punch_time).ToList();

                    return attendanceList;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<GetEmployeeDataVM>> GetEmployeeData()
        {
            try
            {
                var endPoint = "/personnel/api/employees/?page_size=1000000";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://10.1.0.10:80/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("jwt", GetToken());
                    HttpResponseMessage result = await client.GetAsync(endPoint);
                    if (result.IsSuccessStatusCode)
                    {
                        var employeeData = JsonConvert.DeserializeObject<GolbalOutSourceDataVM<GetEmployeeDataVM>>(await result.Content.ReadAsStringAsync()).data.ToList();
                        return employeeData;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private string GetToken()
        {
            StringContent data = new StringContent(JsonConvert.SerializeObject(new { username = "admin", password = "admin123" }),
                                    Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                using (var response = client.PostAsync("http://10.1.0.10:80/jwt-api-token-auth/", data).Result)
                {
                    var r = response.Content.ReadAsStringAsync().Result;
                    var d = JsonConvert.DeserializeObject<jsonobj>(r);

                    return d.token;
                }
            }
        }
    }
}
