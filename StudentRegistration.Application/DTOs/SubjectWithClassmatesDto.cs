namespace StudentRegistration.Application.DTOs;

public class ClassmateDto
{
    public string StudentName { get; set; } = string.Empty;
}

public class SubjectWithClassmatesDto
{
    public string SubjectName { get; set; } = string.Empty;
    public List<ClassmateDto> Classmates { get; set; } = new();
}