namespace WebApplication2.Dto;

public class ProjectCreateDto
{
    public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
   
}