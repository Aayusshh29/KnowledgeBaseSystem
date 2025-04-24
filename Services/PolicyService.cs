using Backend.Data;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Backend.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly ApplicationDbContext _context;
        private static readonly TimeZoneInfo _ist =
            TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public PolicyService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------- Helper Methods ----------
        // Get current time in IST and format it to string
        private static string GetCurrentTimeInIstFormat()
        {
            DateTime currentTimeInIst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _ist);
            return currentTimeInIst.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-IN"));
        }

        // Get all policies, excluding the CreatedBy navigation property
        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            return await _context.Policies
                .Select(p => new Policy
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,  // Stored as a formatted string
                    CreatedById = p.CreatedById
                }).ToListAsync();
        }

        // Get a single policy by ID, excluding the CreatedBy navigation property
        public async Task<Policy?> GetPolicyByIdAsync(int id)
        {
            return await _context.Policies
                .Where(p => p.Id == id)
                .Select(p => new Policy
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,  // Stored as a formatted string
                    CreatedById = p.CreatedById
                }).FirstOrDefaultAsync();
        }

        // Create a new policy, automatically setting CreatedAt to the current IST time
        public async Task<Policy> CreatePolicyAsync(Policy policy)
        {
            policy.CreatedAt = GetCurrentTimeInIstFormat();  // Use the helper method for IST formatting
            policy.LastUpdatedAt = null;  // Initially, there is no LastUpdatedAt (set to null)

            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
            return policy;
        }

        // Update an existing policy
        public async Task<Policy?> UpdatePolicyAsync(int id, Policy updatedPolicy)
        {
            var existingPolicy = await _context.Policies.FindAsync(id);
            if (existingPolicy == null) return null;

            existingPolicy.Title = updatedPolicy.Title;
            existingPolicy.Content = updatedPolicy.Content;
            existingPolicy.CreatedById = updatedPolicy.CreatedById;

            // Update LastUpdatedAt to the current IST time
            existingPolicy.LastUpdatedAt = GetCurrentTimeInIstFormat();  // Format the time for update

            await _context.SaveChangesAsync();
            return existingPolicy;
        }

        // Delete a policy
        public async Task<bool> DeletePolicyAsync(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null) return false;

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
