using Microsoft.EntityFrameworkCore;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;
using StudentRegistration.Infrastructure.Persistence;

namespace StudentRegistration.Infrastructure.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly ApplicationDbContext _context;

        public EstudianteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task<Estudiante> CreateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<bool> UpdateAsync(Estudiante estudiante)
        {
            var existing = await _context.Estudiantes.FindAsync(estudiante.Id);
            if (existing == null) return false;

            existing.FirstName = estudiante.FirstName;
            existing.LastName = estudiante.LastName;
            existing.Email = estudiante.Email;

            _context.Estudiantes.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return false;

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}