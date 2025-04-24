using Backend.Dtos;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IProcedureService
    {
        Task<IEnumerable<Procedure>> GetAllProceduresAsync();
        Task<Procedure?> GetProcedureByIdAsync(int id);

        // actor = user performing the action (e.g. HttpContext.User.Identity.Name ?? "system")
        Task<Procedure> CreateProcedureAsync(ProcedureCreateDto dto, string actor);
        Task<Procedure?> UpdateProcedureAsync(int id, ProcedureUpdateDto dto, string actor);

        Task<bool> DeleteProcedureAsync(int id);
    }
}
