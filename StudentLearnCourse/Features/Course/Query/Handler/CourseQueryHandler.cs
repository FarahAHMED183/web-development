using CRUD_Operation.Features.Course.Query.Models;

namespace CRUD_Operation.Features.Course.Query.Handler
{
    public class CourseQueryHandler : IRequestHandler<GetAllCoursesDto, Response>,
                                      IRequestHandler<GetCourseByIdDto, Response>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllCoursesDto request, CancellationToken cancellationToken)
        {
            var coursesResult = await _courseRepository.GetCoursesWithStudentsPagedAsync(request.PageNumber, request.PageSize);
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
                Message = "Courses retrieved successfully",
                Data = paginatedResult
            };
        }

        public async Task<Response> Handle(GetCourseByIdDto request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetCourseWithStudentsByIdAsync(request.Id);
            if (course == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Course not found"
                };
            }

            var courseDto = _mapper.Map<CourseWithStudentsDto>(course);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Course retrieved successfully",
                Data = courseDto
            };
        }
    }
}
