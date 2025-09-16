namespace CRUD_Operation.Features.Course.Query.Models
{
    public class SearchCoursesRequest : IRequest<Response>
    {
        public string? Name { get; set; }
        public int? Hours { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
