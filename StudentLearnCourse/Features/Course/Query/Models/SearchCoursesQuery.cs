namespace CRUD_Operation.Features.Course.Query.Models
{
    public class SearchCoursesQuery
    {
        public string? Name { get; set; }
        public int? Hours { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
