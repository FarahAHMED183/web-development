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
            var course = _mapper.Map<CourseEntity>(request);
            await _courseRepository.Create(course);
            
            return new Response
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Course created successfully",
                Data = course
            };
        }

        public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
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

        public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
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
    }
}
