namespace CRUD_Operation.Features.Course.Query.Models
{
    public class CourseWithStudentsDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Cname { get; set; } = string.Empty;
        public int Hours { get; set; }
        public List<EnrolledStudentDto> EnrolledStudents { get; set; } = new List<EnrolledStudentDto>();
    }

    public class EnrolledStudentDto
    {
        public int Id { get; set; }
        public string SID { get; set; } = string.Empty;
        public string Sname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Grade { get; set; } = string.Empty;
    }
}
