using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Application.DTOs;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;

namespace StudentRegistration.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _profesorService;

    public ProfesoresController(IProfesorService profesorService)
    {
        _profesorService = profesorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfesorDto>>> GetAll()
    {
        var profesores = await _profesorService.GetAllAsync();

        var result = profesores.Select(p => new ProfesorDto
        {
            Id = p.Id,
            Nombre = $"{p.FirstName} {p.LastName}"
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfesorDto>> GetById(int id)
    {
        var prof = await _profesorService.GetByIdAsync(id);
        if (prof is null) return NotFound();

        var result = new ProfesorDto
        {
            Id = prof.Id,
            Nombre = $"{prof.FirstName} {prof.LastName}"
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProfesorDto>> Create([FromBody] CreateProfesorDto dto)
    {
        var profesor = new Profesor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var created = await _profesorService.CreateAsync(profesor);

        var result = new ProfesorDto
        {
            Id = created.Id,
            Nombre = $"{created.FirstName} {created.LastName}"
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProfesorDto dto)
    {
        var profesor = new Profesor
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var updated = await _profesorService.UpdateAsync(profesor);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _profesorService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}