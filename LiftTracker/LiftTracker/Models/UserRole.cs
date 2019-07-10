using Microsoft.AspNetCore.Identity;

namespace LiftTracker.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public  AppUser AppUser { get; set; }

        public Role Role { get; set; }
    }
}