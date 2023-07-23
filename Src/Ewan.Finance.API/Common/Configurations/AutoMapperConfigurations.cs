using AutoMapper;

namespace Ewan.Finance.API.Common.Configurations
{
    public class AutoMapperConfigurations
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            return config.CreateMapper();
        }
    }
}
