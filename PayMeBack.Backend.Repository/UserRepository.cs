using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts;
using System;

namespace PayMeBack.Backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private PayMeBackContext _context;

        public UserRepository(PayMeBackContext context)
        {
            _context = context;
        }

        public AppUser Create(AppUser user)
        {
            _context.AppUsers.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
