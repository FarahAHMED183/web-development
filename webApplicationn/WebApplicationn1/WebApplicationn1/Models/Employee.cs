namespace WebApplicationn1.Models;

public class Employee
{
 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public Login Login { get; set; }
    
}