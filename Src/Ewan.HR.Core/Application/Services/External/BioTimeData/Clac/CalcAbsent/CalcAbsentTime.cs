namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAbsent
{
    public class CalcAbsentTime : ICalcAbsentTime
    {
        public async Task<int> ClacCallcenterAbsentTime(DateTime start, DateTime end)
        {
            var absentTime = 0;
            if (start.TimeOfDay.Hours >= 12)
            {
                if (start.TimeOfDay.Hours >= 8)
                {
                    absentTime = (start.TimeOfDay.Hours - 8) * 60 - start.Minute;
                }

                if (end.TimeOfDay.Hours < 16)
                {
                    absentTime += (16 - end.TimeOfDay.Hours) * 60 - end.Minute;
                }
            }
            else
            {
                if (start.TimeOfDay.Hours >= 14)
                {
                    absentTime = (start.TimeOfDay.Hours - 14) * 60 - start.Minute;
                }
                if (end.TimeOfDay.Hours < 22)
                {
                    absentTime += (22 - end.TimeOfDay.Hours) * 60 - end.Minute;
                }
            }
            return await Task.FromResult(absentTime);
        }

        public async Task<int> ClacMuqeemAbsentTime(DateTime start, DateTime end)
        {
            var absentTime = 0;
            switch (start.DayOfWeek.ToString())
            {
                case "Saturday":
                    if (start.TimeOfDay.Hours >= 10)
                    {
                        absentTime = (start.TimeOfDay.Hours - 10) * 60 - start.TimeOfDay.Minutes;
                    }
                    if (end.TimeOfDay.Hours < 14)
                    {
                        absentTime += (14 - end.TimeOfDay.Hours) * 60 - end.TimeOfDay.Minutes;
                    }
                    break;
                case "Thursday":
                    if (start.TimeOfDay.Hours >= 8)
                    {
                        absentTime = (start.TimeOfDay.Hours - 8) * 60 - start.TimeOfDay.Minutes;
                    }
                    if (end.TimeOfDay.Hours < 16)
                    {
                        absentTime += (16 - end.TimeOfDay.Hours) * 60 - end.TimeOfDay.Minutes;
                    }
                    break;
                default:
                    if (start.TimeOfDay.Hours >= 8)
                    {
                        absentTime = (start.TimeOfDay.Hours - 8) * 60 - start.TimeOfDay.Minutes;
                    }

                    else if (end.TimeOfDay.Hours < 17)
                    {
                        absentTime += (17 - end.TimeOfDay.Hours) * 60 - end.TimeOfDay.Minutes;
                    }
                    break;
            }
            return await Task.FromResult(absentTime);
        }

        public async Task<int> ClacSaudiAbsentTime(DateTime start, DateTime end)
        {
            var absentTime = 0;
            if (start.TimeOfDay.Hours >= 8)
            {
                absentTime = (start.TimeOfDay.Hours - 8) * 60 - start.TimeOfDay.Minutes;
            }
            if (end.TimeOfDay.Hours < 16)
            {
                absentTime += (16 - end.TimeOfDay.Hours) * 60 - end.TimeOfDay.Minutes;
            }
            return await Task.FromResult(absentTime);
        }

        public async Task<int> ClacShowroomsAbsentTime(DateTime start, DateTime end)
        {
            var absentTime = 0;
            if (start.TimeOfDay.Hours <= 12)
            {
                if (start.TimeOfDay.Hours >= 9)
                {
                    absentTime = (start.Hour - 9) * 60 - start.Minute;
                }
                if (end.Hour < 17)
                {
                    absentTime = (17 - end.Hour) * 60 - end.Minute;
                }
            }
            else
            {
                if (start.TimeOfDay.Hours >= 14)
                {
                    absentTime = (start.Hour - 14) * 60 - start.Minute;
                }
                if (end.Hour < 22)
                {
                    absentTime = (22 - end.Hour) * 60 - end.Minute;
                }
            }

            return await Task.FromResult(absentTime);
        }
    }
}
