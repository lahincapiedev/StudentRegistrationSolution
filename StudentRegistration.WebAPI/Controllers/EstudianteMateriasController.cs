using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Application.DTOs;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;

namespace StudentRegistration.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudianteMateriasController : ControllerBase
{
    private readonly IEstudianteMateriaService _service;

    public EstudianteMateriasController(IEstudianteMateriaService service)
    {
        _service = service;
    }

    [HttpPost("{estudianteId}")]
    public async Task<IActionResult> AsignarMaterias(int estudianteId, [FromBody] List<int> materiaIds)
    {
        var resultado = await _service.AsignarMateriasAsync(estudianteId, materiaIds);
        return resultado ? Ok("Materias asignadas correctamente") : BadRequest("No se pudieron asignar las materias");
    }

    [HttpGet("{estudianteId}")]
    public async Task<ActionResult<List<Materia>>> ObtenerMaterias(int estudianteId)
    {
        var materias = await _service.ObtenerMateriasPorEstudianteAsync(estudianteId);
        return Ok(materias);
    }
    [HttpGet("profesor/{profesorId}")]
    public async Task<ActionResult<List<MateriaDto>>> ObtenerMateriasPorProfesor(int profesorId)
    {
        var materias = await _service.ObtenerMateriasPorProfesorAsync(profesorId);
        return Ok(materias);
    }
}