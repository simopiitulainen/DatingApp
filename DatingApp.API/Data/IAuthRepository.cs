using System.Threading.Tasks;
using DatingApp.API.Models;

/*
    Autentikointirajapinta tarjoaa metodit käyttäjän kirjautumiseen & rekisteröintiin
 */

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);        
    }

}