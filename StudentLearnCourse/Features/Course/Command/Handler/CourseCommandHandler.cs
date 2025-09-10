namespace CRUD_Operation.Features.Course.Command.Handler
{
    public class CourseCommandHandler : IRequestHandler<CourseDto, Response>,
                                        IRequestHandler<UpdateCourseDto, Response>,
                                        IRequestHandler<DeleteCourseDto, Response>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _mapper.Map<CourseEntity>(request);
                await _courseRepository.Create(course);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Course created successfully",
                    Data = course
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error creating course: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCourse = await _courseRepository.GetById(request.Id);
                if (existingCourse == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Course not found"
                    };
                }

                _mapper.Map(request, existingCourse);
                await _courseRepository.Update(existingCourse);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Course updated successfully",
                    Data = existingCourse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error updating course: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetById(request.Id);
                if (course == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Course not found"
                    };
                }

                await _courseRepository.Delete(course);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Course deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error deleting course: {ex.Message}"
                };
            }
        }
    }
}
