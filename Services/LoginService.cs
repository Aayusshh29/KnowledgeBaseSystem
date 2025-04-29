using Backend.Data;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<string> LoginAsync(Login Login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Login.Email);
            if (user == null)
            {
                return "Invalid email or password";
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password!, Login.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return "User login successful";
            }
            else
            {
                return "Invalid email or password";
            }
        }
    }
}
