namespace WebApplicationn1.Models;

public class Login
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}