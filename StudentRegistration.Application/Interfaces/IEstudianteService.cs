using StudentRegistration.Core.Entities;

namespace StudentRegistration.Application.Interfaces;

public interface IEstudianteService
{
    Task<List<Estudiante>> GetAllAsync();
    Task<Estudiante?> GetByIdAsync(int id);
    Task<Estudiante> CreateAsync(Estudiante estudiante);
    Task<bool> UpdateAsync(Estudiante estudiante);
    Task<bool> DeleteAsync(int id);
}