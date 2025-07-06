namespace StudentRegistration.Core.Entities;

public class EstudianteMateria
{
    public int Id { get; set; }

    public int EstudianteId { get; set; }
    public Estudiante Estudiante { get; set; } = default!;

    public int MateriaId { get; set; }
    public Materia Materia { get; set; } = default!;
}