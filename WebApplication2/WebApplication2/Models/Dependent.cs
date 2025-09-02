namespace WebApplication2.Models;

public class Dependent
{
    public int Id { get; set; }
    public string D_Name { get; set; }
    public string Gender { get; set; }
    public string Relationship { get; set; }

    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
}
