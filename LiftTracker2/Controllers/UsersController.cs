using System.Threading.Tasks;
using LiftTracker.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LiftTracker2.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }
     [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            
            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == userId);
            return Ok(user);
        } 

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var UsersToReturn = await _context.AppUsers.ToArrayAsync();
            return Ok(UsersToReturn);
        }
    }
}
