using System.Collections.Generic;

namespace LiftTracker.Models
{
    public class Exercise
    {
        public string Id { get; set; }
        public string NameOfExercise { get; set; }

        public string BodyPart { get; set; }

        public string TypeOfExercise { get; set; }
        public int Repetitions { get; set; }

    }
}