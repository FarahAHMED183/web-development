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
            try
            {
                var courses = await _courseRepository.GetCoursesWithStudentsAsync();
                var coursesDto = _mapper.Map<List<CourseWithStudentsDto>>(courses);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Courses retrieved successfully",
                    Data = coursesDto
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving courses: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(GetCourseByIdDto request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving course: {ex.Message}"
                };
            }
        }
    }
}
