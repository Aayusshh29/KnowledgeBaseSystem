using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IPolicyRequestService
    {
        Task<IEnumerable<PolicyRequest>> GetAllPolicyRequestsAsync();
        Task<PolicyRequest?> GetPolicyRequestByIdAsync(int id);
        Task<PolicyRequest> CreatePolicyRequestAsync(PolicyRequest policyRequest);
        Task<PolicyRequest?> UpdatePolicyRequestAsync(int id, PolicyRequest updatedPolicyRequest);
        Task<bool> DeletePolicyRequestAsync(int id);
    }
}
