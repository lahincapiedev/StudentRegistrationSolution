using StudentRegistration.Core.Entities;

namespace StudentRegistration.Application.Interfaces;

public interface IProfesorService
{
    Task<List<Profesor>> GetAllAsync();
    Task<Profesor?> GetByIdAsync(int id);
    Task<Profesor> CreateAsync(Profesor profesor);
    Task<bool> UpdateAsync(Profesor profesor);
    Task<bool> DeleteAsync(int id);
}