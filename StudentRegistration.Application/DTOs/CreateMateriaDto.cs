namespace StudentRegistration.Application.DTOs
{
    public class CreateMateriaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int ProfesorId { get; set; }
    }
}