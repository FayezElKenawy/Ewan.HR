namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAbsent
{
    public interface ICalcAbsentTime
    {
        Task<int> ClacSaudiAbsentTime(DateTime start, DateTime end);
        Task<int> ClacMuqeemAbsentTime(DateTime start, DateTime end);
        Task<int> ClacCallcenterAbsentTime(DateTime start, DateTime end);
        Task<int> ClacShowroomsAbsentTime(DateTime start, DateTime end);
    }
}
