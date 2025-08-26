namespace WebApplicationn1.Dto;

public class EmployeeCreateDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public string DepartmentId { get; set; }
    public string RoleId { get; set; }
    public string UserId { get; set; }
}