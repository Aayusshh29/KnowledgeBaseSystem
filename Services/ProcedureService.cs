using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Backend.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly ApplicationDbContext _context;
        private static readonly TimeZoneInfo _ist =
            TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public ProcedureService(ApplicationDbContext context) =>
            _context = context;

        // ---------- Helpers ----------

        // Get the current time in IST
        private static DateTime NowIst() =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _ist);

        // Convert UTC to IST and return in Indian date format
        private static string GetCurrentTimeInIstFormat()
        {
            // Get the current time in IST
            DateTime currentTimeInIst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _ist);

            // Return formatted time in dd/MM/yyyy hh:mm tt format
            return currentTimeInIst.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-IN"));
        }

        // ---------- Queries ----------
        public async Task<IEnumerable<Procedure>> GetAllProceduresAsync() =>
            await _context.Procedures.AsNoTracking().ToListAsync();

        public async Task<Procedure?> GetProcedureByIdAsync(int id) =>
            await _context.Procedures.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

        // ---------- Create ----------
        public async Task<Procedure> CreateProcedureAsync(
            ProcedureCreateDto dto, string actor)
        {
            var entity = new Procedure
            {
                Title = dto.Title,
                Steps = dto.Steps,
                Department = dto.Department,
                CreatedAt = GetCurrentTimeInIstFormat(),  // Use current time in IST when creating
                CreatedBy = dto.CreatedBy ?? actor,
                LastUpdatedAt = null
            };

            _context.Procedures.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // ---------- Update ----------
        public async Task<Procedure?> UpdateProcedureAsync(
            int id, ProcedureUpdateDto dto, string actor)
        {
            var entity = await _context.Procedures.FindAsync(id);
            if (entity is null) return null;

            entity.Title = dto.Title ?? entity.Title;
            entity.Steps = dto.Steps ?? entity.Steps;
            entity.Department = dto.Department ?? entity.Department;

            // Use current time in IST for LastUpdatedAt when updating
            entity.LastUpdatedAt = GetCurrentTimeInIstFormat();
            await _context.SaveChangesAsync();
            return entity;
        }

        // ---------- Delete ----------
        public async Task<bool> DeleteProcedureAsync(int id)
        {
            var entity = await _context.Procedures.FindAsync(id);
            if (entity is null) return false;

            _context.Procedures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------- Convert Procedure to ProcedureReadDto ----------
        public ProcedureReadDto ToReadDto(Procedure p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Steps = p.Steps,
            Department = p.Department,
            CreatedAt = GetCurrentTimeInIstFormat(),  // Formatting when converting to DTO
            LastUpdatedAt = GetCurrentTimeInIstFormat(),  // Formatting when converting to DTO
            CreatedBy = p.CreatedBy
        };
    }
}
