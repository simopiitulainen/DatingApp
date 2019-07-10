using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LiftTracker.Models
{
    public class AppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        // public ICollection<Message> MessagesReceived { get; set; }

        // public ICollection<Message> MessagesSent { get; set; }

        // public ICollection<Photo> Photos { get; set; }

        // public ICollection<TrainingProgram> TrainingPrograms { get; set; }
        // public ICollection<AppUser> Trainee { get; set; }

        // public string CoachId { get; set; }
        // public AppUser Coach { get; set; }

    }
}