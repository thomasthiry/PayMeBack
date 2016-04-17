using AutoMapper;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Web.Models;

namespace PayMeBack.Backend.Web.Configurations
{
    public static class MapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Split, SplitDto>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}
