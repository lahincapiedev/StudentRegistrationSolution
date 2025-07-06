namespace StudentRegistration.Core.Entities;

public class Estudiante
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<EstudianteMateria> EstudianteMaterias { get; set; } = new List<EstudianteMateria>();
}