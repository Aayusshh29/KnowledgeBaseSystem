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

        public async Task<User?> LoginAsync(Login Login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Login.Email);
            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password!, Login.Password);

            if (result == PasswordVerificationResult.Success)
            {
                // Do NOT expose password
                user.Password = null;
                return user;
            }

            return null;
        }
    }
}
