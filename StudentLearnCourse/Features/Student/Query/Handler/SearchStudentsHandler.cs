namespace CRUD_Operation.Features.Student.Query.Handler
{
    public class SearchStudentsHandler : IRequestHandler<SearchStudentsRequest, Response>
    {
        private readonly IGenericRepository<StudentEntity> _genericRepository;
        private readonly IMapper _mapper;

        public SearchStudentsHandler(IGenericRepository<StudentEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(SearchStudentsRequest request, CancellationToken cancellationToken)
        {
            // Create specification based on search criteria
            var specification = new StudentSpecification();

            if (!string.IsNullOrEmpty(request.Name))
                specification.AddCriteria(s => s.Sname.Contains(request.Name));

            if (request.Age.HasValue)
                specification.AddCriteria(s => s.Age >= request.Age.Value);

            // Use generic repository with specification and pagination
            var studentsResult = await _genericRepository.GetPagedAsync(
                request.PageNumber, 
                request.PageSize, 
                specification);

            var studentsDto = _mapper.Map<List<StudentWithCoursesDto>>(studentsResult.Items);
            
            var paginatedResult = new PaginatedResult<StudentWithCoursesDto>(
                studentsDto,
                studentsResult.PageNumber,
                studentsResult.PageSize,
                studentsResult.TotalRecords,
                studentsResult.TotalPages);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Students searched successfully",
                Data = paginatedResult
            };
        }
    }
}
