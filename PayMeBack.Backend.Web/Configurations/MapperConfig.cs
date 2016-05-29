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
                cfg.CreateMap<AppUser, UserDto>();
                cfg.CreateMap<UserAndToken, UserAndTokenDto>();
                cfg.CreateMap<Split, SplitDto>();
                cfg.CreateMap<Contact, ContactDto>();
                cfg.CreateMap<SplitContact, SplitContactDto>()
                    .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Contact.Email))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Contact.Name));
                cfg.CreateMap<Settlement, SettlementDto>();
                cfg.CreateMap<SettlementTransfer, SettlementTransferDto>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}
