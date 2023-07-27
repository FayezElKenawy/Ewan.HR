using AutoMapper;
using Ewan.HR.Core.Application.Models.Attendance;

namespace Ewan.HR.API.Mappers.AutoMapper.Attendance
{
    public class AttendanceProfile:Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceDataVM,AttendanceDataDTO>().ReverseMap();
        }
    }
}
