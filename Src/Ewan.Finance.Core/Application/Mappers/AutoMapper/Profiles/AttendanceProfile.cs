using AutoMapper;
using Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Domain.Entities.Attendance;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<IList<GetAttendanceVM>, AttendanceDataVM>().AfterMap<AttendanceMappingConfig>();
            CreateMap<AttendanceDataVM, EmployeeAttendanceLog>();
            CreateMap<EmployeeAttendanceLog, AttendanceDataVM>();
        }
    }
}
