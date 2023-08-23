using AutoMapper;
using Ewan.HR.API.Dtos.Attendance;
using Ewan.HR.API.Dtos.PayRoll;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Models.PayRoll;

namespace Ewan.HR.API.Mappers.AutoMapper.Attendance
{
    public class AttendanceProfile:Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceDataVM,AttendanceDataDTO>().ReverseMap();
            CreateMap<PayRollAddVM, PayRollAddDTO>().ReverseMap();
            CreateMap<MonthSettingsVM,MonthSettingsDTO>().ReverseMap();
            CreateMap<GetMonthSettingsDTO, GetMonthSettingsVM>().ReverseMap();
        }
    }
}
