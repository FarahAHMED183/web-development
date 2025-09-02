namespace WebApplication2.Models;

public class Department
{
    public int D_No { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
}
