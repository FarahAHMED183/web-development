namespace CRUD_Operation.Features.Learn.Query.Models
{
    public class GetEnrollmentsByStudentDto : IRequest<Response>
    {
        public int StudentId { get; set; }
    }
}
