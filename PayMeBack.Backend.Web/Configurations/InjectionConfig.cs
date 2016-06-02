using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Repository;
using PayMeBack.Backend.Services;
using PayMeBack.Backend.Web.Models;

namespace PayMeBack.Backend.Web.Configurations
{
    public static class InjectionConfig
    {
        public static void ConfigureCustomServices(IServiceCollection services)
        {
            // Scoped lifetime (per request) is used to prevent concurrence issues with EF: "There is already an open DataReader associated with this Command which must be closed first."
            services.AddScoped<ISplitService, SplitService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IGenericRepository<Split>, GenericRepository<Split>>();
            services.AddScoped<IGenericRepository<Contact>, GenericRepository<Contact>>();
            services.AddScoped<IGenericRepository<SplitContact>, GenericRepository<SplitContact>>();
            services.AddScoped<IGenericRepository<AppUser>, GenericRepository<AppUser>>();

            var mapper = MapperConfig.CreateMapper();
            services.AddInstance<IMapper>(mapper);
        }
    }
}
