namespace StudentRegistration.Core.Entities;

public class Profesor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public ICollection<Materia> Materias { get; set; } = new List<Materia>();
}