using Backend.Data;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class FAQService : IFAQService
    {
        private readonly ApplicationDbContext _context;

        public FAQService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FAQ>> GetAllFAQsAsync()
        {
            return await _context.FAQs.ToListAsync();
        }

        public async Task<FAQ?> GetFAQByIdAsync(int id)
        {
            return await _context.FAQs.FindAsync(id);
        }

        public async Task<FAQ> CreateFAQAsync(FAQ faq)
        {
            _context.FAQs.Add(faq);
            await _context.SaveChangesAsync();
            return faq;
        }

        public async Task<FAQ?> UpdateFAQAsync(int id, FAQ updatedFaq)
        {
            var existingFaq = await _context.FAQs.FindAsync(id);
            if (existingFaq == null) return null;

            existingFaq.Question = updatedFaq.Question;
            existingFaq.Answer = updatedFaq.Answer;

            await _context.SaveChangesAsync();
            return existingFaq;
        }

        public async Task<bool> DeleteFAQAsync(int id)
        {
            var faq = await _context.FAQs.FindAsync(id);
            if (faq == null) return false;

            _context.FAQs.Remove(faq);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
