namespace CRUD_Operation.Features.Course.Command.Models
{
    public class CourseDto : IRequest<Response>
    {
        public string Code { get; set; }
        public string Cname { get; set; }
        public int Hours { get; set; }
    }
}
