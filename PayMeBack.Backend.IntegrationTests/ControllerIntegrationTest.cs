//using AutoMapper;
//using Microsoft.Data.Entity;
//using Microsoft.Extensions.DependencyInjection;
//using PayMeBack.Backend.Contracts;
//using PayMeBack.Backend.Contracts.Services;
//using PayMeBack.Backend.Models;
//using PayMeBack.Backend.Repository;
//using PayMeBack.Backend.Web.Configurations;
//using PayMeBack.Backend.Web.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PayMeBack.Backend.IntegrationTests
//{
//    public class ControllerIntegrationTest
//    {
//        protected ControllerWithContext<T> CreateController<T>()
//        {
//            var services = new ServiceCollection();
//            services
//                .AddEntityFramework()
//                .AddInMemoryDatabase()
//                .AddDbContext<PayMeBackContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase());

//            InjectionConfig.ConfigureCustomServices(services);

//            var serviceProvider = services.BuildServiceProvider();
//            var context = CreateContext(serviceProvider);

//            services.AddTransient<IGenericRepository<Split>, GenericRepository<Split>>(provider => new GenericRepository<Split>(context));
//            services.AddTransient<IGenericRepository<SplitContact>, GenericRepository<SplitContact>>(provider => new GenericRepository<SplitContact>(context));
//            services.AddTransient<ContactController>();

//            var mapper = serviceProvider.GetService<IMapper>();
//            var splitService = serviceProvider.GetService<IContactService>();

//            var controller = serviceProvider.GetService<ContactController>(); //new T(mapper, splitService);

//            return new ControllerWithContext<ContactController> { Controller = controller, Context = context };
//        }

//        protected PayMeBackContext CreateContext(IServiceProvider serviceProvider)
//        {
//            var context = serviceProvider.GetService<PayMeBackContext>();

//            context.Splits.Add(new Split { Name = "Today" });
//            context.Splits.Add(new Split { Name = "Yesterday" });

//            context.Contacts.Add(new Contact { Name = "John" });
//            context.Contacts.Add(new Contact { Name = "Mark" });

//            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 1, Owes = 50m });
//            context.SplitContacts.Add(new SplitContact { SplitId = 1, ContactId = 2, Paid = 50m });
//            context.SaveChanges();

//            return context;
//        }
//    }
//}
