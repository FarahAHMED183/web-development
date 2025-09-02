namespace WebApplication2.Dto;

public class ProjectDto
{
    public int ProjectId { get; set; }
    public string? Name { get; set; }
    public decimal Budget { get; set; }

    public int DepartmentId { get; set; }
    public DepartmentDto? Department { get; set; }
    public List<EmployeeDto>? Employees { get; set; }
}