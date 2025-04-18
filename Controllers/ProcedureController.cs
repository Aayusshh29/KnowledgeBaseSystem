using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureService _procedureService;

        public ProcedureController(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        // GET: api/procedure
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var procedures = await _procedureService.GetAllProceduresAsync();
            return Ok(procedures);
        }

        // GET: api/procedure/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var procedure = await _procedureService.GetProcedureByIdAsync(id);
            if (procedure == null) return NotFound();
            return Ok(procedure);
        }

        // POST: api/procedure
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Procedure procedure)
        {
            var createdProcedure = await _procedureService.CreateProcedureAsync(procedure);
            return CreatedAtAction(nameof(GetById), new { id = createdProcedure.Id }, createdProcedure);
        }

        // PUT: api/procedure/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Procedure procedure)
        {
            var updatedProcedure = await _procedureService.UpdateProcedureAsync(id, procedure);
            if (updatedProcedure == null) return NotFound();
            return Ok(updatedProcedure);
        }

        // DELETE: api/procedure/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _procedureService.DeleteProcedureAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
