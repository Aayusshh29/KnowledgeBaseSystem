using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login Login)
        {
            var result = await _loginService.LoginAsync(Login);

            if (result == "User login successful")
            {
                return Ok(new { message = result });
            }

            return Unauthorized(new { message = result });
        }
    }
}
