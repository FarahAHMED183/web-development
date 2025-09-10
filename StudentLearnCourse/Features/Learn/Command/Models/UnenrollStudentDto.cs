namespace CRUD_Operation.Features.Learn.Command.Models
{
    public class UnenrollStudentDto : IRequest<Response>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
