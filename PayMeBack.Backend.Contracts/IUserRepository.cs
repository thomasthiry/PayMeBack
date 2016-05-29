using Microsoft.AspNet.Identity;
using PayMeBack.Backend.Models;

namespace PayMeBack.Backend.Contracts
{
    public interface IUserRepository
    {
        AppUser Create(AppUser user);
    }
}
