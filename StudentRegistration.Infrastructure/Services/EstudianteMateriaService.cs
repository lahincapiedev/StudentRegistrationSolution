using Microsoft.EntityFrameworkCore;
using StudentRegistration.Application.DTOs;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;
using StudentRegistration.Infrastructure.Persistence;

namespace StudentRegistration.Infrastructure.Services;

public class EstudianteMateriaService : IEstudianteMateriaService
{
    private readonly ApplicationDbContext _context;

    public EstudianteMateriaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AsignarMateriasAsync(int estudianteId, List<int> materiaIds)
    {
        ValidarCantidadMaterias(materiaIds);

        var materias = await ObtenerMateriasConProfesor(materiaIds);
        ValidarProfesoresUnicos(materias);

        await EliminarMateriasAnteriores(estudianteId);
        await AsignarNuevasMaterias(estudianteId, materiaIds);

        return true;
    }
    private void ValidarCantidadMaterias(List<int> materiaIds)
    {
        if (materiaIds.Count != 3)
            throw new Exception("El estudiante debe seleccionar exactamente 3 materias.");
    }

    private async Task<List<Materia>> ObtenerMateriasConProfesor(List<int> materiaIds)
    {
        var materias = await _context.Materias
            .Include(m => m.Profesor)
            .Where(m => materiaIds.Contains(m.Id))
            .ToListAsync();

        if (materias.Count != 3)
            throw new Exception("Algunas materias no existen.");

        return materias;
    }

    private void ValidarProfesoresUnicos(List<Materia> materias)
    {
        var profesoresUnicos = materias.Select(m => m.ProfesorId).Distinct().Count();
        if (profesoresUnicos < 3)
            throw new Exception("No se puede registrar más de una materia con el mismo profesor.");
    }

    private async Task EliminarMateriasAnteriores(int estudianteId)
    {
        var actuales = await _context.EstudianteMaterias
            .Where(em => em.EstudianteId == estudianteId)
            .ToListAsync();

        _context.EstudianteMaterias.RemoveRange(actuales);
    }

    private async Task AsignarNuevasMaterias(int estudianteId, List<int> materiaIds)
    {
        foreach (var id in materiaIds)
        {
            _context.EstudianteMaterias.Add(new EstudianteMateria
            {
                EstudianteId = estudianteId,
                MateriaId = id
            });
        }

        await _context.SaveChangesAsync();
    }
    public async Task<List<MateriaDto>> ObtenerMateriasPorEstudianteAsync(int estudianteId)
    {
        var materias = await _context.EstudianteMaterias
            .Where(em => em.EstudianteId == estudianteId)
            .Include(em => em.Materia)
                .ThenInclude(mat => mat.Profesor)
            .Select(em => new MateriaDto
            {
                Id = em.Materia.Id,
                Nombre = em.Materia.Nombre,
                ProfesorNombre = $"{em.Materia.Profesor.FirstName} {em.Materia.Profesor.LastName}"
            })
            .ToListAsync();

        return materias;
    }
    public async Task<List<MateriaDto>> ObtenerMateriasPorProfesorAsync(int profesorId)
    {
        var materias = await _context.Materias
            .Where(m => m.ProfesorId == profesorId)
            .Select(m => new MateriaDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                ProfesorNombre = $"{m.Profesor.FirstName} {m.Profesor.LastName}"
            })
            .ToListAsync();

        return materias;
    }
    public async Task<List<SubjectWithClassmatesDto>> GetClassmatesPerSubjectAsync(int studentId)
    {
        var materiasDelEstudiante = await _context.EstudianteMaterias
            .Where(em => em.EstudianteId == studentId)
            .Include(em => em.Materia)
                .ThenInclude(m => m.EstudianteMaterias)
                    .ThenInclude(em => em.Estudiante)
            .ToListAsync();

        var resultado = materiasDelEstudiante
            .Select(em => new SubjectWithClassmatesDto
            {
                SubjectName = em.Materia.Nombre,
                Classmates = em.Materia.EstudianteMaterias
                    .Where(otro => otro.EstudianteId != studentId)
                    .Select(otro => new ClassmateDto
                    {
                        StudentName = $"{otro.Estudiante.FirstName} {otro.Estudiante.LastName}"
                    }).ToList()
            })
            .ToList();

        return resultado;
    }
}