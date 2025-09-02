namespace WebApplication2.Dto;


    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string? Name { get; set; }

        public List<EmployeeDto>? Employees { get; set; }
        public List<ProjectDto>? Projects { get; set; }
    }

