using LiftTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiftTracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers {get; set;}

        // public DbSet<Photo> Photos { get; set; }

        // public DbSet<Message> Messages { get; set; }

       // public DbSet<AppUser> Trainee { get; set; }

        // public DbSet<TrainingProgram> TrainingPrograms { get; set; }

      //  public DbSet<Exercise> Exercises { get; set; }

        //  protected override void OnModelCreating(ModelBuilder builder)       
        // {
        //   base.OnModelCreating(builder);

        //   builder.Entity<Message>()
        //   .HasOne(u => u.Sender)
        //   .WithMany(m => m.MessagesSent)
        //   .OnDelete(DeleteBehavior.Restrict);

        //   builder.Entity<Message>()
        //   .HasOne(u => u.Recipient)
        //   .WithMany(m => m.MessagesReceived)
        //   .OnDelete(DeleteBehavior.Restrict);
  
        //   } 

        
    }
}

