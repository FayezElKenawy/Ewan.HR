using AutoMapper;
using Ewan.HR.Core.Application.Models.PayRoll;
using Ewan.HR.Core.Domain.Entities.PayRoll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class PayRollProfile:Profile
    {
        public PayRollProfile()
        {
            CreateMap<PayRollAddVM,PayRollData>().ReverseMap();
        }
    }
}
