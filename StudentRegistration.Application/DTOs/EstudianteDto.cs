﻿namespace StudentRegistration.Application.DTOs
{
    public class EstudianteDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<MateriaDto> Materias { get; set; } = new();
    }
}