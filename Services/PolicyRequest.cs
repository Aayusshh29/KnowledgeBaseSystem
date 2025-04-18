using Backend.Data;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PolicyRequestService : IPolicyRequestService
    {
        private readonly ApplicationDbContext _context;

        public PolicyRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolicyRequest>> GetAllPolicyRequestsAsync()
        {
            return await _context.PolicyRequests.ToListAsync();  // No need to include RequestedBy
        }

        public async Task<PolicyRequest?> GetPolicyRequestByIdAsync(int id)
        {
            return await _context.PolicyRequests.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PolicyRequest> CreatePolicyRequestAsync(PolicyRequest policyRequest)
        {
            _context.PolicyRequests.Add(policyRequest);
            await _context.SaveChangesAsync();
            return policyRequest;
        }

        public async Task<PolicyRequest?> UpdatePolicyRequestAsync(int id, PolicyRequest updatedPolicyRequest)
        {
            var existingPolicyRequest = await _context.PolicyRequests.FindAsync(id);
            if (existingPolicyRequest == null) return null;

            existingPolicyRequest.Title = updatedPolicyRequest.Title;
            existingPolicyRequest.Content = updatedPolicyRequest.Content;
            existingPolicyRequest.Status = updatedPolicyRequest.Status;
            existingPolicyRequest.RequestedAt = updatedPolicyRequest.RequestedAt;
            existingPolicyRequest.RequestedById = updatedPolicyRequest.RequestedById;

            await _context.SaveChangesAsync();
            return existingPolicyRequest;
        }

        public async Task<bool> DeletePolicyRequestAsync(int id)
        {
            var policyRequest = await _context.PolicyRequests.FindAsync(id);
            if (policyRequest == null) return false;

            _context.PolicyRequests.Remove(policyRequest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
