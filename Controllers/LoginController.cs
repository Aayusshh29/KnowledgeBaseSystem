using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{

    //Endpoint : http://localhost:5041/api/login
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
            var user = await _loginService.LoginAsync(Login);

            if (user != null)
            {
                return Ok(user); // Return full user info
            }

            return Unauthorized(new { message = "Invalid email or password" });
        }
    }
}
