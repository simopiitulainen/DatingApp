using LiftTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiftTracker2.Controllers
{
     [Route("api/[controller]")]

    public class ExercisesController : ControllerBase
    {

        private readonly DataContext _context;
        public ExercisesController(DataContext context)
        {
            _context = context;

        }

    }

}
            
/* 
        }
        [HttpGet]
        public async Task<IActionResult> GetExercises() 
        {
            var exercises = await _context.Exercises.ToListAsync()
        }
    }
} */