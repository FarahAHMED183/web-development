namespace CRUD_Operation.Features.Course.Command.Models
{
    public class UpdateCourseDto : IRequest<Response>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Cname { get; set; }
        public int Hours { get; set; }
    }
}
