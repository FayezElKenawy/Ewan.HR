namespace Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcOvertime
{
    public interface ICalcOvertime
    {
        Task<int> ClacSaudiOvertime(DateTime start, DateTime end);
        Task<int> ClacMuqeemOvertime(DateTime start, DateTime end);
        Task<int> ClacCallcenterOvertime(DateTime start, DateTime end);
        Task<int> ClacShowroomsOvertime(DateTime start, DateTime end);
    }
}
