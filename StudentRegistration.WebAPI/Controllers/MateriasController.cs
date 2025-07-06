using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Application.DTOs;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;

namespace StudentRegistration.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly IMateriaService _materiaService;

    public MateriasController(IMateriaService materiaService)
    {
        _materiaService = materiaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MateriaDto>>> GetAll()
    {
        var materias = await _materiaService.GetAllAsync();
        var result = materias.Select(m => new MateriaDto
        {
            Id = m.Id,
            Nombre = m.Nombre,
            ProfesorNombre = m.Profesor != null
                ? $"{m.Profesor.FirstName} {m.Profesor.LastName}"
                : string.Empty
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MateriaDto>> GetById(int id)
    {
        var m = await _materiaService.GetByIdAsync(id);
        if (m is null) return NotFound();

        var dto = new MateriaDto
        {
            Id = m.Id,
            Nombre = m.Nombre,
            ProfesorNombre = m.Profesor != null
                ? $"{m.Profesor.FirstName} {m.Profesor.LastName}"
                : string.Empty
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<MateriaDto>> Create([FromBody] CreateMateriaDto dto)
    {
        var materia = new Materia
        {
            Nombre = dto.Nombre,
            ProfesorId = dto.ProfesorId
        };

        var created = await _materiaService.CreateAsync(materia);

        var result = new MateriaDto
        {
            Id = created.Id,
            Nombre = created.Nombre,
            ProfesorNombre = string.Empty // se podría completar con datos si se incluye el profesor en el Include
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateMateriaDto dto)
    {
        var materia = new Materia
        {
            Id = id,
            Nombre = dto.Nombre,
            ProfesorId = dto.ProfesorId
        };

        var updated = await _materiaService.UpdateAsync(materia);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _materiaService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}