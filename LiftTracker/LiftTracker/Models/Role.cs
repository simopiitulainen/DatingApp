using System.Collections.Generic;

namespace LiftTracker.Models
{
    public class Role
    {
         public ICollection<UserRole>  UserRoles { get; set; }
    }
}