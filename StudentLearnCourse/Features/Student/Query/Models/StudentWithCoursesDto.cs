namespace CRUD_Operation.Features.Student.Query.Models
{
    public class StudentWithCoursesDto
    {
        public int Id { get; set; }
        public string SID { get; set; } = string.Empty;
        public string Sname { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<EnrolledCourseDto> EnrolledCourses { get; set; } = new List<EnrolledCourseDto>();
    }

    public class EnrolledCourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Cname { get; set; } = string.Empty;
        public int Hours { get; set; }
        public string Grade { get; set; } = string.Empty;
    }
}
