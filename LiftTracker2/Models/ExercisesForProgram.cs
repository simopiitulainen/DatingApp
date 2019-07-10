using System.Collections.Generic;

namespace LiftTracker.Models
{
    public class ExercisesForProgram
    {
        public int Id { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public TrainingProgram TrainingProgram { get; set; }

        public int TrainingProgramId { get; set; }
    }
}