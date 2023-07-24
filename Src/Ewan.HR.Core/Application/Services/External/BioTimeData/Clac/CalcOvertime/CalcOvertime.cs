namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcOvertime
{
    public class CalcOvertime : ICalcOvertime
    {
        public async Task<int> ClacCallcenterOvertime(DateTime start, DateTime end)
        {
            var overTime = 0;
            if (start.TimeOfDay.Hours >= 12)
            {
                if (start.TimeOfDay.Hours < 8)
                {
                    overTime = (8 - start.TimeOfDay.Hours) * 60 - start.Minute;
                }

                if (end.TimeOfDay.Hours >= 16)
                {
                    overTime += (end.TimeOfDay.Hours - 16) * 60 + end.Minute;
                }
            }
            else
            {
                if (start.TimeOfDay.Hours < 14 && start.TimeOfDay.Hours >= 12)
                {
                    overTime = (2 - start.TimeOfDay.Hours) * 60 - start.Minute;
                }
                if (end.TimeOfDay.Hours >= 22)
                {
                    overTime += (end.TimeOfDay.Hours - 22) * 60 + end.Minute;
                }
            }
            return await Task.FromResult(overTime);
        }

        public async Task<int> ClacMuqeemOvertime(DateTime start, DateTime end)
        {
            var overTime = 0;
            switch (start.DayOfWeek.ToString())
            {
                case "Saturday":
                    if (start.TimeOfDay.Hours < 10)
                    {
                        overTime = (10 - start.TimeOfDay.Hours) * 60 - start.Minute;
                    }
                    if (end.TimeOfDay.Hours >= 14)
                    {
                        overTime += (end.TimeOfDay.Hours - 14) * 60 + end.Minute;
                    }
                    break;
                case "Thursday":
                    if (start.TimeOfDay.Hours < 8)
                    {
                        overTime = (8 - start.TimeOfDay.Hours) * 60 - start.Minute;
                    }
                    if (end.TimeOfDay.Hours >= 16)
                    {
                        overTime += (end.TimeOfDay.Hours - 16) * 60 + end.Minute;
                    }
                    break;
                default:
                    if (start.TimeOfDay.Hours < 8)
                    {
                        overTime = (8 - start.TimeOfDay.Hours) * 60 - start.Minute;
                    }
                    else if (end.TimeOfDay.Hours >= 17 && end.DayOfWeek.ToString() != "Saturday")
                    {
                        overTime += (end.TimeOfDay.Hours - 17) * 60 + end.Minute;
                    }
                    break;
            }
            
            return await Task.FromResult(overTime);
        }

        public async Task<int> ClacSaudiOvertime(DateTime start, DateTime end)
        {
            var overTime = 0;
            if (start.TimeOfDay.Hours < 8)
            {
                overTime = (8 - start.TimeOfDay.Hours) * 60 - start.Minute;
            }
            if (end.TimeOfDay.Hours >= 16)
            {
                overTime += (end.TimeOfDay.Hours - 16) * 60 + end.Minute;
            }
            return await Task.FromResult(overTime);
        }

        public async Task<int> ClacShowroomsOvertime(DateTime start, DateTime end)
        {
            var overTime = 0;
            if (start.TimeOfDay.Hours <= 12)
            {
                if (start.TimeOfDay.Hours < 9)
                {
                    overTime = (9 - start.Hour) * 60 - start.Minute;
                }
                if (end.Hour >= 17)
                {
                    overTime = (end.Hour - 17) * 60 + end.Minute;
                }
            }
            else
            {
                if (start.TimeOfDay.Hours < 14 && start.TimeOfDay.Hours >= 12)
                {
                    overTime = (14 - start.Hour) * 60 - start.Minute;
                }
                if (end.Hour >= 22)
                {
                    overTime = (end.Hour - 22) * 60 + end.Minute;
                }
            }
            return await Task.FromResult(overTime);
        }
    }
}
