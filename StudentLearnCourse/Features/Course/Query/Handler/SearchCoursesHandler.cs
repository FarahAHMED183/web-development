namespace CRUD_Operation.Features.Course.Query.Handler
{
    public class SearchCoursesHandler : IRequestHandler<SearchCoursesRequest, Response>
    {
        private readonly IGenericRepository<CourseEntity> _genericRepository;
        private readonly IMapper _mapper;

        public SearchCoursesHandler(IGenericRepository<CourseEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(SearchCoursesRequest request, CancellationToken cancellationToken)
        {
            // Create specification based on search criteria
            var specification = new CourseSpecification();

            if (!string.IsNullOrEmpty(request.Name))
                specification.AddCriteria(c => c.Cname.Contains(request.Name));

            if (request.Hours.HasValue)
                specification.AddCriteria(c => c.Hours >= request.Hours.Value);

            // Use generic repository with specification and pagination
            var coursesResult = await _genericRepository.GetPagedAsync(
                request.PageNumber, 
                request.PageSize, 
                specification);

            var coursesDto = _mapper.Map<List<CourseWithStudentsDto>>(coursesResult.Items);
            
            var paginatedResult = new PaginatedResult<CourseWithStudentsDto>(
                coursesDto,
                coursesResult.PageNumber,
                coursesResult.PageSize,
                coursesResult.TotalRecords,
                coursesResult.TotalPages);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Courses searched successfully",
                Data = paginatedResult
            };
        }
    }
}
