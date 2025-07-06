namespace StudentRegistration.Core.Entities;

public class Materia
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public int Creditos { get; set; } = 3;

    public int ProfesorId { get; set; }
    public Profesor Profesor { get; set; } = default!;

    public ICollection<EstudianteMateria> EstudianteMaterias { get; set; } = new List<EstudianteMateria>();
}