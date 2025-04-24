using Backend.Dtos;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    // Endpoint: http://localhost:5041/api/procedure
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureService _procedureService;

        public ProcedureController(IProcedureService procedureService) =>
            _procedureService = procedureService;

        // helper – who is making the call? Defaults to "system" if the actor is not available.
        private string Actor => User?.Identity?.Name ?? "system";

        // ------------------------------------------------------------
        // GET: api/procedure
        // ------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var procedures = await _procedureService.GetAllProceduresAsync();
            return Ok(procedures);
        }

        // ------------------------------------------------------------
        // GET: api/procedure/{id}
        // ------------------------------------------------------------
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var procedure = await _procedureService.GetProcedureByIdAsync(id);
            return procedure is null ? NotFound() : Ok(procedure);
        }

        // ------------------------------------------------------------
        // POST: api/procedure
        // Body → ProcedureCreateDto
        // ------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProcedureCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Create procedure with the actor information for CreatedBy and CreatedAt
            var created = await _procedureService.CreateProcedureAsync(dto, Actor);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ------------------------------------------------------------
        // PUT: api/procedure/{id}
        // Body → ProcedureUpdateDto (no audit fields)
        // ------------------------------------------------------------
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProcedureUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Update procedure with the actor information for LastUpdatedAt and LastUpdatedBy
            var updated = await _procedureService.UpdateProcedureAsync(id, dto, Actor);
            return updated is null ? NotFound() : Ok(updated);
        }

        // ------------------------------------------------------------
        // DELETE: api/procedure/{id}
        // ------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _procedureService.DeleteProcedureAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
