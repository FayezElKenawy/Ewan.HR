using AutoMapper;
using Ewan.HR.API.Dtos.PayRoll;
using Ewan.HR.Core.Application.Models.PayRoll;

namespace Ewan.HR.API.Mappers.AutoMapper.PayRoll
{
    public class PAyRollProfile:Profile
    {
        public PAyRollProfile()
        {
            CreateMap<PayRollAddVM,PayRollAddDTO>().ReverseMap();
        }
    }
}
