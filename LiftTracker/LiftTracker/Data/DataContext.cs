using LiftTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiftTracker.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<AppUser> Trainee { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
          base.OnModelCreating(builder);

          builder.Entity<UserRole>(userRole => {
            userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

            userRole.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles) 
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

            
            userRole.HasOne(ur => ur.AppUser)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();


             builder.Entity<Message>()
          .HasOne(u => u.Sender)
          .WithMany(m => m.MessagesSent)
          .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<Message>()
          .HasOne(u => u.Recipient)
          .WithMany(m => m.MessagesReceived)
          .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<AppUser>()
          .HasOne(au => au.Coach);

    
          });

    }
}
}