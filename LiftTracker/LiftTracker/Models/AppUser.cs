using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LiftTracker.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser Coach { get; set; }

         public AppUser Trainee { get; set; }

        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<Message> MessagesSent { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<Photo> Photos { get; set; }

    }
}