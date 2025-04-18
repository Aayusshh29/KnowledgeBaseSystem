using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IProcedureService
    {
        Task<IEnumerable<Procedure>> GetAllProceduresAsync();
        Task<Procedure?> GetProcedureByIdAsync(int id);
        Task<Procedure> CreateProcedureAsync(Procedure procedure);
        Task<Procedure?> UpdateProcedureAsync(int id, Procedure updatedProcedure);
        Task<bool> DeleteProcedureAsync(int id);
    }
}
