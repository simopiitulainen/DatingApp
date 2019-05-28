namespace DatingApp.API.Models
{
    /*
        Luokka käyttäjä-objektille. 
     */
    public class User
    {
        public int Id {get; set;}

        public string Username{ get; set; }

        public byte[] PasswordHash {get; set;}

        public byte[] PasswordSalt {get; set;}
    }
}