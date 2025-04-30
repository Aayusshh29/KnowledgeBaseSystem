using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    // Endpoint: http://localhost:5041/api/policy
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // GET all policies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var policies = await _policyService.GetAllPoliciesAsync();
            var result = policies.Select(p => new
            {
                p.Id,
                p.Title,
                p.Department,
                p.Content,
                p.CreatedAt,
                p.LastUpdatedAt,
                p.CreatedById
            }).ToList();

            return Ok(result);
        }

        // GET policy by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var policy = await _policyService.GetPolicyByIdAsync(id);
            if (policy == null) return NotFound();

            var result = new
            {
                policy.Id,
                policy.Title,
                policy.Department,
                policy.Content,
                policy.CreatedAt,
                policy.LastUpdatedAt,
                policy.CreatedById
            };

            return Ok(result);
        }

        // POST create new policy
        [HttpPost]
        public async Task<IActionResult> Create(Policy policy)
        {
            // Set CreatedAt automatically before saving the policy
            policy.CreatedAt = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm tt");  // Format current time for IST

            var createdPolicy = await _policyService.CreatePolicyAsync(policy);
            return CreatedAtAction(nameof(GetById), new { id = createdPolicy.Id }, createdPolicy);
        }

        // PUT update policy
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Policy policy)
        {
            var updatedPolicy = await _policyService.UpdatePolicyAsync(id, policy);
            if (updatedPolicy == null) return NotFound();
            return Ok(updatedPolicy);
        }

        // DELETE policy by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _policyService.DeletePolicyAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
