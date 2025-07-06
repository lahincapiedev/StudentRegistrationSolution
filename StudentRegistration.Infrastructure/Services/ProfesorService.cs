using Microsoft.EntityFrameworkCore;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;
using StudentRegistration.Infrastructure.Persistence;

namespace StudentRegistration.Infrastructure.Services;

public class ProfesorService : IProfesorService
{
    private readonly ApplicationDbContext _context;

    public ProfesorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Profesor>> GetAllAsync()
        => await _context.Profesores.ToListAsync();

    public async Task<Profesor?> GetByIdAsync(int id)
        => await _context.Profesores.FindAsync(id);

    public async Task<Profesor> CreateAsync(Profesor profesor)
    {
        _context.Profesores.Add(profesor);
        await _context.SaveChangesAsync();
        return profesor;
    }

    public async Task<bool> UpdateAsync(Profesor profesor)
    {
        var existing = await _context.Profesores.FindAsync(profesor.Id);
        if (existing == null) return false;

        existing.FirstName = profesor.FirstName;
        existing.LastName = profesor.LastName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor == null) return false;

        _context.Profesores.Remove(profesor);
        await _context.SaveChangesAsync();
        return true;
    }
}