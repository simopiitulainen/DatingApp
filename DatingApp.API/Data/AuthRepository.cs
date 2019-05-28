using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
/*
    Luokka autentikoinnin metodeille.
 */
namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        /*
            Login-metodi kirjaa käyttäjän sisään.
            Hakee käyttäjänimen kannasta, jos ei löydy, palauttaa null. 
            Jos käyttäjä löytyy, mutta salasanan hash ei matchaa kannassa olevaan hashiin, palauttaa null.

            Muuten palauttaa user-objektin. 
         */
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username); 
            if (user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

            /*
                vertaa annetun salasanan ja kannassa olevan salasanan hash-arvoja. 
             */
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
             
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++){
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        /*
            Luo käyttäjän salasanalle Saltin ja  hash-arvon. 
         */
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
           
        }
        /*
        
            Tarkastaa, onko käyttäjää olemassa. 
         */
        public async Task<bool> UserExists(string username)
        {
           if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}