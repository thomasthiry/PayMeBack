using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PayMeBack.Backend.Web.Configurations;
using Microsoft.Data.Entity;
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

            InjectionConfig.ConfigureCustomServices(services);

            var connection = @"Server=(localdb)\mssqllocaldb;Database=PayMeBack_dev;Trusted_Connection=True;";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<PayMeBackContext>(options => options
                    .UseSqlServer(connection)
                    .MigrationsAssembly("PayMeBack.Backend.Repository")
                );
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
                routes.MapRoute(name: "Login", template: "login", defaults: new { controller = "User", action = "Login" });
                routes.MapRoute(name: "UserCreate", template: "users", defaults: new { controller = "User", action = "UserCreate" });

                routes.MapRoute(name: "SplitsList", template: "splits", defaults: new { controller = "Split", action = "List" });
                routes.MapRoute(name: "SplitGet", template: "splits/{id:int}", defaults: new { controller = "Split", action = "Get" });
                routes.MapRoute(name: "SplitCreate", template: "splits", defaults: new { controller = "Split", action = "Create" });
                routes.MapRoute(name: "SplitSettle", template: "splits/{id:int}/settle", defaults: new { controller = "Split", action = "Settle" });

                routes.MapRoute(name: "SplitContactsList", template: "splits/{splitId:int}/contacts", defaults: new { controller = "Contact", action = "ListSplitContactsBySplit" });
                routes.MapRoute(name: "ContactCreate", template: "splits/{splitId:int}/contacts", defaults: new { controller = "Contact", action = "CreateIfNeededAndAddToSplit" });
                routes.MapRoute(name: "SplitContactGet", template: "splits/{splitId:int}/contacts/{splitContactId:int}", defaults: new { controller = "Contact", action = "GetSplitContact" });
                routes.MapRoute(name: "SplitContactUpdate", template: "splits/{splitId:int}/contacts/{splitContactId:int}", defaults: new { controller = "Contact", action = "UpdateSplitContact" });

            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
