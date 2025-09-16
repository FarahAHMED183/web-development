namespace CRUD_Operation.Features.Learn.Query.Models
{
    public class GetAllEnrollmentsDto : IRequest<Response>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
