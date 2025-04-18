using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyRequestController : ControllerBase
    {
        private readonly IPolicyRequestService _policyRequestService;

        public PolicyRequestController(IPolicyRequestService policyRequestService)
        {
            _policyRequestService = policyRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var policyRequests = await _policyRequestService.GetAllPolicyRequestsAsync();
            return Ok(policyRequests);  // Now no need to include RequestedBy
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var policyRequest = await _policyRequestService.GetPolicyRequestByIdAsync(id);
            if (policyRequest == null) return NotFound();
            return Ok(policyRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PolicyRequest policyRequest)
        {
            if (policyRequest == null) return BadRequest("Policy request data is required");
            var createdPolicyRequest = await _policyRequestService.CreatePolicyRequestAsync(policyRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdPolicyRequest.Id }, createdPolicyRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PolicyRequest policyRequest)
        {
            if (policyRequest == null) return BadRequest("Policy request data is required");

            var updatedPolicyRequest = await _policyRequestService.UpdatePolicyRequestAsync(id, policyRequest);
            if (updatedPolicyRequest == null) return NotFound();
            return Ok(updatedPolicyRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _policyRequestService.DeletePolicyRequestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
