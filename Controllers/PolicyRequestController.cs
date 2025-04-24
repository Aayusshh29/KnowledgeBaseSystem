using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    // Endpoint: http://localhost:5041/api/policyrequest
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyRequestController : ControllerBase
    {
        private readonly IPolicyRequestService _policyRequestService;

        public PolicyRequestController(IPolicyRequestService policyRequestService)
        {
            _policyRequestService = policyRequestService;
        }

        // ---------- Helper Method ----------
        // Helper method to format current UTC time to string in "dd/MM/yyyy hh:mm tt" format
        private string GetFormattedUtcNow()
        {
            return DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-IN"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var policyRequests = await _policyRequestService.GetAllPolicyRequestsAsync();
            return Ok(policyRequests);  // Returning the list of policy requests
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var policyRequest = await _policyRequestService.GetPolicyRequestByIdAsync(id);
            if (policyRequest == null) return NotFound();
            return Ok(policyRequest);  // Returning the specific policy request by id
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PolicyRequest policyRequest)
        {
            if (policyRequest == null) return BadRequest("Policy request data is required");

            // Set RequestedAt to the current UTC time in the desired string format
            policyRequest.RequestedAt = GetFormattedUtcNow();  // Automatically set to formatted string

            var createdPolicyRequest = await _policyRequestService.CreatePolicyRequestAsync(policyRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdPolicyRequest.Id }, createdPolicyRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PolicyRequest policyRequest)
        {
            if (policyRequest == null) return BadRequest("Policy request data is required");

            // Fetch the existing policy request from the database
            var existingPolicyRequest = await _policyRequestService.GetPolicyRequestByIdAsync(id);
            if (existingPolicyRequest == null) return NotFound();

            // Update only the necessary fields. Do not modify RequestedAt.
            existingPolicyRequest.Title = policyRequest.Title;
            existingPolicyRequest.Content = policyRequest.Content;
            existingPolicyRequest.Status = policyRequest.Status;
            existingPolicyRequest.RequestedById = policyRequest.RequestedById;

            // Save the updated policy request to the database
            var updatedPolicyRequest = await _policyRequestService.UpdatePolicyRequestAsync(id, existingPolicyRequest);

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
