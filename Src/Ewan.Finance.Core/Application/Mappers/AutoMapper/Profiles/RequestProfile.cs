using AutoMapper;
using Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig;
using Ewan.HR.Core.Application.Models.Employee;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Domain.Entities.Request.MasterData;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<AddRequestVM, RequestMasterData>().AfterMap((s, d) =>
            {
                d.SupervisorId = s.DirectManagerId;
            });
            CreateMap<EmployeeVM, AddRequestVM>().AfterMap<AddRequestMappingConfig>();
            CreateMap<RequestMasterData, RequestVM>().AfterMap<RequestMappingConfig>();
            CreateMap<RequestType, TypesVM>();
            CreateMap<RequestVM, RequestMasterData>().AfterMap((source, dest) =>
            {
                if (source.Status == "جديد")
                {
                    dest.Status = byte.Parse("0");
                }
                else
                    dest.Status = byte.Parse("1");
            });


        }
    }
}
