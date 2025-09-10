namespace CRUD_Operation.Features.Learn.Command.Models
{
    public class EnrollStudentDto : IRequest<Response>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string? Grade { get; set; }
    }
}
