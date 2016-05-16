using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using PayMeBack.Backend.Models;

namespace PayMeBack.Backend.Repository
{
    public class PayMeBackContext : DbContext
    {
        public PayMeBackContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Split> Splits { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SplitContact> SplitContacts { get; set; }
    }

}
