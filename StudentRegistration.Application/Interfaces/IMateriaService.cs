using StudentRegistration.Core.Entities;

namespace StudentRegistration.Application.Interfaces;

public interface IMateriaService
{
    Task<List<Materia>> GetAllAsync();
    Task<Materia?> GetByIdAsync(int id);
    Task<Materia> CreateAsync(Materia materia);
    Task<bool> UpdateAsync(Materia materia);
    Task<bool> DeleteAsync(int id);
}