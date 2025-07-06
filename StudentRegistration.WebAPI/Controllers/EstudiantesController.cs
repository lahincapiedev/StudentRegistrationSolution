using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Application.DTOs;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Core.Entities;

namespace StudentRegistration.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetAll()
        {
            var estudiantes = await _estudianteService.GetAllAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetById(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null) return NotFound();
            return Ok(estudiante);
        }

        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<Estudiante>> Create([FromBody] CreateEstudianteDto dto)
        {
            var nuevoEstudiante = new Estudiante
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var created = await _estudianteService.CreateAsync(nuevoEstudiante);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEstudianteDto dto)
        {
            var estudiante = new Estudiante
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var updated = await _estudianteService.UpdateAsync(estudiante);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _estudianteService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}