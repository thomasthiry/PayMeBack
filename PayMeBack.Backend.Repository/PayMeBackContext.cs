﻿using Microsoft.Data.Entity;
using PayMeBack.Backend.Models;

namespace PayMeBack.Backend.Repository
{
    public class PayMeBackContext : DbContext
    {
        public DbSet<Split> Splits { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<SplitContact> SplitContacts { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
    }

}
