using AutoMapper;
using Ewan.HR.Core.Application.Enums.Global;
using Ewan.HR.Core.Application.Models.Request.Vacation;
using Ewan.HR.Core.Domain.Entities.Vacation;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.MapperConfig.RequestMappingConfig
{
    internal class VacationEditMappingConfig : IMappingAction<VacationDetailsVM, VacationRequest>
    {

        public void Process(VacationDetailsVM source, VacationRequest destination, ResolutionContext context)
        {
            if (source.ActionFrom == ActionFromEnum.Employee.ToString())
            {
                destination.StartDate = source.StartDate;
                destination.EndDate = source.EndDate;
                destination.TotalDayes = source.TotalDayes;
            }
            else if (source.ActionFrom == ActionFromEnum.Supervisor.ToString())
            {
                destination.ReplacingEmployeeNumber = source.ReplacingEmployeeNumber;
                destination.ReplacingEmployeeName = source.ReplacingEmployeeName;
                destination.WorkHandover = source.WorkHandover;
            }
            else if (source.ActionFrom == ActionFromEnum.HR.ToString())
            {
                destination.Balance = source.Balance;
                destination.RemainingBalance = source.RemainingBalance;
                destination.VacationDays = source.VacationDays;
            }
        }
    }
}
