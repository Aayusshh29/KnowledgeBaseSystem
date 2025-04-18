using Backend.Data;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly ApplicationDbContext _context;

        public ProcedureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Procedure>> GetAllProceduresAsync()
        {
            return await _context.Procedures.ToListAsync();
        }

        public async Task<Procedure?> GetProcedureByIdAsync(int id)
        {
            return await _context.Procedures.FindAsync(id);
        }

        public async Task<Procedure> CreateProcedureAsync(Procedure procedure)
        {
            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();
            return procedure;
        }

        public async Task<Procedure?> UpdateProcedureAsync(int id, Procedure updatedProcedure)
        {
            var existingProcedure = await _context.Procedures.FindAsync(id);
            if (existingProcedure == null) return null;

            existingProcedure.Title = updatedProcedure.Title;
            existingProcedure.Steps = updatedProcedure.Steps;

            await _context.SaveChangesAsync();
            return existingProcedure;
        }

        public async Task<bool> DeleteProcedureAsync(int id)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null) return false;

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
