namespace CRUD_Operation.Features.Course.Query.Models
{
    public class GetCourseByIdDto : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
