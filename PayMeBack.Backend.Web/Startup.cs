using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Services;
using PayMeBack.Backend.Web.Configurations;
using Microsoft.Data.Entity;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Repository;

namespace PayMeBack.Backend.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
            .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddCors();

            // Scoped lifetime (per request) is used to prevent concurrence issues with EF: "There is already an open DataReader associated with this Command which must be closed first."
            services.AddScoped<ISplitService, SplitService>();
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IGenericRepository<Split>, GenericRepository<Split>>();
            services.AddScoped<IGenericRepository<Contact>, GenericRepository<Contact>>();

            var mapper = MapperConfig.CreateMapper();
            services.AddInstance<IMapper>(mapper);

            var connection = @"Server=(localdb)\mssqllocaldb;Database=PayMeBack_dev;Trusted_Connection=True;";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<PayMeBackContext>(options => options.UseSqlServer(connection)
                .MigrationsAssembly("PayMeBack.Backend.Repository"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "SplitsList", template: "splits", defaults: new { controller = "Split", action = "List" });
                routes.MapRoute(name: "SplitGet", template: "splits/{id:int}", defaults: new { controller = "Split", action = "Get" });
                routes.MapRoute(name: "SplitCreate", template: "splits", defaults: new { controller = "Split", action = "Create" });

                routes.MapRoute(name: "SplitContactsList", template: "splits/{splitId:int}/contacts", defaults: new { controller = "Contact", action = "ListBySplit" });
                //routes.MapRoute(name: "SplitContactsGet", template: "contacts/{id:int}", defaults: new { controller = "Contact", action = "Get" });
                routes.MapRoute(name: "ContactCreate", template: "splits/{splitId:int}/contacts", defaults: new { controller = "Contact", action = "CreateIfNeededAndAddToSplit" });
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
