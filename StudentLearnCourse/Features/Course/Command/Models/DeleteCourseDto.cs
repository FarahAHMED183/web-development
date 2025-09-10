namespace CRUD_Operation.Features.Course.Command.Models
{
    public class DeleteCourseDto : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
