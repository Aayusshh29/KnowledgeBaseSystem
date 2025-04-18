using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IFAQService
    {
        Task<IEnumerable<FAQ>> GetAllFAQsAsync();
        Task<FAQ?> GetFAQByIdAsync(int id);
        Task<FAQ> CreateFAQAsync(FAQ faq);
        Task<FAQ?> UpdateFAQAsync(int id, FAQ updatedFaq);
        Task<bool> DeleteFAQAsync(int id);
    }
}
