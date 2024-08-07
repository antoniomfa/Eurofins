using DataAccess.Entities;
using DataAccess.Repo;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepo _repo;

        public UserController(IRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var authenticatedUser = await _repo.Authenticate(user);

            if (authenticatedUser == false)
                return BadRequest(new { Message = "Password is incorrect" });

            return Ok();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            try
            {
                await _repo.Register(user);
            }
            catch (Exception ex)
            {
                //
            }

            return Ok();
        }
    }
}
