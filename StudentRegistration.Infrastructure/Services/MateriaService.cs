using Microsoft.EntityFrameworkCore;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;
using StudentRegistration.Infrastructure.Persistence;

namespace StudentRegistration.Infrastructure.Services;

public class MateriaService : IMateriaService
{
    private readonly ApplicationDbContext _context;

    public MateriaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Materia>> GetAllAsync()
        => await _context.Materias.Include(m => m.Profesor).ToListAsync();

    public async Task<Materia?> GetByIdAsync(int id)
        => await _context.Materias.Include(m => m.Profesor).FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Materia> CreateAsync(Materia materia)
    {
        _context.Materias.Add(materia);
        await _context.SaveChangesAsync();
        return materia;
    }

    public async Task<bool> UpdateAsync(Materia materia)
    {
        var existing = await _context.Materias.FindAsync(materia.Id);
        if (existing == null) return false;

        existing.Nombre = materia.Nombre;
        existing.Creditos = materia.Creditos;
        existing.ProfesorId = materia.ProfesorId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var materia = await _context.Materias.FindAsync(id);
        if (materia == null) return false;

        _context.Materias.Remove(materia);
        await _context.SaveChangesAsync();
        return true;
    }
}