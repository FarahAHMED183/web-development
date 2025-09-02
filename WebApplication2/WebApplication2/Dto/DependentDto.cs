namespace WebApplication2.Dto;

public class DependentDto
{
    public int DependentId { get; set; }
    public string? Name { get; set; }
    public string? Relationship { get; set; }

    public int EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
}