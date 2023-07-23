using AutoMapper;
using Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig;
using Ewan.HR.Core.Application.Models.Request;
using Ewan.HR.Core.Application.Models.Request.Vacation;
using Ewan.HR.Core.Domain.Entities.Vacation;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class VacationRequestProfile : Profile
    {
        public VacationRequestProfile()
        {

            #region vacation
            CreateMap<AddVacationVm, VacationRequest>().AfterMap<AddVacationMappingConfig>();
            CreateMap<AddVacationVm, AddRequestVM>();
            CreateMap<VacationRequest, VacationDetailsVM>().AfterMap<VacationDetailsConfig>();
            CreateMap<VacationDetailsVM, VacationRequest>().AfterMap<VacationEditMappingConfig>(); ;
            CreateMap<VacationDetailsVM, RequestVM>();
            #endregion
        }
    }
}
