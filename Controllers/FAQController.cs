using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    // Endpoint: http://localhost:5041/api/faq
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var faqs = await _faqService.GetAllFAQsAsync();
            return Ok(faqs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var faq = await _faqService.GetFAQByIdAsync(id);
            if (faq == null) return NotFound();
            return Ok(faq);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FAQ faq)
        {
            if (faq == null) return BadRequest("FAQ data is required");
            var createdFaq = await _faqService.CreateFAQAsync(faq);
            return CreatedAtAction(nameof(GetById), new { id = createdFaq.Id }, createdFaq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FAQ faq)
        {
            if (faq == null) return BadRequest("FAQ data is required");
            var updatedFaq = await _faqService.UpdateFAQAsync(id, faq);
            if (updatedFaq == null) return NotFound();
            return Ok(updatedFaq);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _faqService.DeleteFAQAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
