using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{

     /*
        DataContext-luokka toimii lähteenä kaikille objekteille tietokannassa. Sisältää tiedot 
        kaikista kantaan tallettettavista objekteista. Toimiin ns linkkinä tietokannan ja ohjelmakoodin välillä (ORM, object relational mapper)
      */
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Value> Values {get; set; }
        public DbSet<User> Users {get; set; }
        public DbSet<Photo> Photos{ get; set; }

    }
}