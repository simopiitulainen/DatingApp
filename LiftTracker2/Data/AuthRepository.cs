using System;
using System.Threading.Tasks;
using LiftTracker.Data;
using LiftTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace LiftTracker2.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
           _context = context;

        }
        /* 
            Kirjaa käyttäjän sisään
         */
        public async Task<AppUser> Login(string username, string password)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
              using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {  
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return  false;
                }
            }
            return true;
        }


        /* 
            Rekisteröi käyttäjän palveluun
        */
        public async Task<AppUser> Register(AppUser appUser, string password)
        {
            byte [] PasswordHash, PasswordSalt;
            CreatePasswordHash(password, out PasswordHash, out PasswordSalt);

            appUser.PasswordHash = PasswordHash;
            appUser.PasswordSalt = PasswordSalt;

            await _context.AppUsers.AddAsync(appUser);
            await _context.SaveChangesAsync();
            return appUser;   
        }

        /* 
            Luo Hashin ja saltin rekisteröitävälle käyttäjälle
         */
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        /* 
        Tarkastaa, onko käyttäjä olemassa
         */
        public async Task<bool> UserExists(string email)
        {
            if (await _context.AppUsers.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }
    }
}