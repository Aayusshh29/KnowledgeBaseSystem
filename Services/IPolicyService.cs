using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetAllPoliciesAsync();
        Task<Policy?> GetPolicyByIdAsync(int id);
        Task<Policy> CreatePolicyAsync(Policy policy);
        Task<Policy?> UpdatePolicyAsync(int id, Policy updatedPolicy);
        Task<bool> DeletePolicyAsync(int id);
    }
}
