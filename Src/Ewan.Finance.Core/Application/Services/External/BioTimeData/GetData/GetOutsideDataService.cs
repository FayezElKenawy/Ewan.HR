using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.Global;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Ewan.HR.Core.Application.Services.External.BioTimeData.GetData
{
    public class GetOutsideDataService : IGetOutsideDataService
    {
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
        public async Task<GlobalReturnVM<GetAttendanceVM>> GetAttendance(string user, string start, string end)
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
                    var EmpAttendance = await Res.Content.ReadAsStringAsync();
                    var attendance = JsonConvert.DeserializeObject<GolbalOutSourceDataVM<GetAttendanceVM>>(EmpAttendance).
                        data.OrderBy(c => c.punch_time).ToList();

                    return new GlobalReturnVM<GetAttendanceVM>
                    {
                        Count = attendance.Count,
                        Message = "Success",
                        Details = attendance
                    };
                }
                else
                {
                    return new GlobalReturnVM<GetAttendanceVM>
                    {
                        Count = 0,
                        Message = "No Data Founded"
                    };
                }
            }
        }

        public async Task<GlobalReturnVM<GetUsersDataVM>> GetEmployeeData()
        {
            var endPoint = "/personnel/api/employees/?page_size=1000000";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://10.1.0.10:80/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("jwt", GetToken());
                HttpResponseMessage Res = await client.GetAsync(endPoint);
                if (Res.IsSuccessStatusCode)
                {
                    var employeeData = JsonConvert.DeserializeObject<GolbalOutSourceDataVM<GetUsersDataVM>>(await Res.Content.ReadAsStringAsync()).data.ToList();

                    return new GlobalReturnVM<GetUsersDataVM>
                    {
                        Count = employeeData.Count,
                        Message = "Success",
                        Details = employeeData
                    };
                }
                else
                {
                    return new GlobalReturnVM<GetUsersDataVM>
                    {
                        Message = "No Data Founded"
                    };
                }
            }

        }
    }
}
