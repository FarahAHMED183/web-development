namespace WebApplication2.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public string Gender { get; set; }
    public string? ImagePath { get; set; }

    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }

    public virtual ICollection<Project> Projects { get; set; }
    public virtual ICollection<Dependent> Dependents { get; set; }
}
