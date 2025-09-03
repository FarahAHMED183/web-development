namespace WebApplication2.Dto;


    public class DepartmentReadDto
    {
        public int D_No { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int? ManagerId { get; set; }
        public string? ManagerName { get; set; }
    }

