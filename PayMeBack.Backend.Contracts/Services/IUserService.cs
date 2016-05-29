using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface IUserService
    {
        AppUser Create(string email, string name, string password);
    }
}
