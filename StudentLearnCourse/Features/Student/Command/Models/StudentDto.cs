namespace CRUD_Operation.Features.Student.Command.Models
{
    public class StudentDto : IRequest<Response>
    {
        public string SID { get; set; }
        public string Sname { get; set; }
        public int Age { get; set; }
    }
}
