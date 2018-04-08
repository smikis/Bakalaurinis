using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = new ApplicationUser
            {
                UserName = "test",
                Email = "tomas@tomas.lt"
            };
            var result = await _userManager.CreateAsync(user, "Test11!");
            if (result.Succeeded)
            {

            }
            return Ok();
        }
    }
}