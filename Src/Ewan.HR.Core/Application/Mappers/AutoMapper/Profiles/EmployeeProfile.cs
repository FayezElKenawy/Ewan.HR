using AutoMapper;
using Ewan.HR.Core.Application.Models;
using Ewan.HR.Core.Domain.Entities;

namespace Ewan.HR.Core.Application.Mappers.AutoMapper.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeData, EmployeeRequestVM>();
        }
    }
}
