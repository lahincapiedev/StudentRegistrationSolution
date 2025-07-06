using StudentRegistration.Application.DTOs;

namespace StudentRegistration.Application.Interfaces
{
    public interface IEstudianteMateriaService
    {
        Task<bool> AsignarMateriasAsync(int estudianteId, List<int> materiaIds);
        Task<List<MateriaDto>> ObtenerMateriasPorEstudianteAsync(int estudianteId);
       
        Task<List<MateriaDto>> ObtenerMateriasPorProfesorAsync(int profesorId);
        Task<List<SubjectWithClassmatesDto>> GetClassmatesPerSubjectAsync(int studentId);
    }
}