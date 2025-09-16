namespace CRUD_Operation.Features.Student.Query.Models
{
    public class SearchStudentsRequest : IRequest<Response>
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
