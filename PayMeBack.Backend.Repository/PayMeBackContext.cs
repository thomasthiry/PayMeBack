using Microsoft.Data.Entity;
using PayMeBack.Backend.Models;

namespace PayMeBack.Backend.Contracts
{
    public class PayMeBackContext : DbContext
    {
        public DbSet<Split> Splits { get; set; }
    }

}
