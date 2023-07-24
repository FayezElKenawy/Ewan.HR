using AutoMapper;
using Ewan.HR.Core.Application.Models.Request.Vacation;
using Ewan.HR.Core.Application.Services.Request.Vacation;
using Ewan.HR.Core.Domain.Entities.Vacation;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig
{
    internal class AddVacationMappingConfig : IMappingAction<AddVacationVm, VacationRequest>
    {
        private readonly IVacationService _vacationService;

        public AddVacationMappingConfig(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }
        public void Process(AddVacationVm source, VacationRequest destination, ResolutionContext context)
        {
            try
            {
                var h = _vacationService.GetMaxId().Result;
                destination.Number = (h + 1).ToString();
            }
            catch (Exception)
            {

                destination.Number = 1.ToString();
            }
        }
    }
}
