namespace CRUD_Operation.Global
{
    public record PaginatedResult<T>(
        IEnumerable<T> Items,
        int PageNumber,
        int PageSize,
        int TotalRecords,
        int TotalPages);
}