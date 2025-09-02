namespace WebApplication2.Models;

public class Project
{
    public int P_No { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
