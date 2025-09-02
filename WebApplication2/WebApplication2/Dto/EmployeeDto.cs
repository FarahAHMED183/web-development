namespace WebApplication2.Dto;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public string Gender { get; set; }
    public string ImageUrl { get; set; }

    public string DepartmentName { get; set; }
}