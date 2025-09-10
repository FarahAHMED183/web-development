namespace CRUD_Operation.Features.Student.Query.Models
{
    public class GetStudentByIdDto : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
