namespace CRUD_Operation.Features.Student.Command.Models
{
    public class UpdateStudentDto : IRequest<Response>
    {
        public int Id { get; set; }
        public string SID { get; set; }
        public string Sname { get; set; }
        public int Age { get; set; }
    }
}
