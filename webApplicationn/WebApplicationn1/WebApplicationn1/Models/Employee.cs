namespace WebApplicationn1.Models;

public class Employee
{
 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public int RoleId { get; set; }
    public virtual Department Department { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
    public virtual Login? Login { get; set; }
    
}