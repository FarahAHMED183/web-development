namespace CRUD_Operation.Features.Learn.Query.Models
{
    public class GetEnrollmentsByCourseDto : IRequest<Response>
    {
        public int CourseId { get; set; }
    }
}
