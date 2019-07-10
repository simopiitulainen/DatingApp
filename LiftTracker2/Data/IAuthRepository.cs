using System.Threading.Tasks;
using LiftTracker.Models;

namespace LiftTracker2.Data
{
    public interface IAuthRepository
    {
         Task<AppUser> Register(AppUser appUser, string password);
         Task<AppUser> Login(string email, string password);
         Task<bool> UserExists(string email);
    }
}