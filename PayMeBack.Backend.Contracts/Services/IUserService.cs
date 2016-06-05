using PayMeBack.Backend.Models;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface IUserService
    {
        AppUser Create(string email, string name, string password);

        UserAndToken Login(string email, string password);

        AppUser GetUserForToken(string token);
    }
}
