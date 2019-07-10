using System;
using System.Collections.Generic;

namespace LiftTracker.Models
{
    public class TrainingProgram

    
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string  AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string CreatorUserId { get; set; }

        public AppUser CreatorUser { get; set; }

        public DateTime DateStarted { get; set; }

        public DateTime DateFinished{ get; set; }

        public ExercisesForProgram ExercisesForProgram { get; set; }

        public int ExercisesForProgramId { get; set; }

        public bool IsCurrent { get; set; }

        public bool IsApprovedByUser { get; set; }

        public bool IsApprovedByCoach { get; set; }
    }
}