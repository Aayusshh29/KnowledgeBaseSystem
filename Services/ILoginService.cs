using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(Login Login);
    }
}
