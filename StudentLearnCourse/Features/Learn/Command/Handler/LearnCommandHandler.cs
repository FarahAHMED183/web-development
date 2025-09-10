namespace CRUD_Operation.Features.Learn.Command.Handler
{
    public class LearnCommandHandler : IRequestHandler<EnrollStudentDto, Response>,
                                       IRequestHandler<UnenrollStudentDto, Response>,
                                       IRequestHandler<UpdateGradeDto, Response>
    {
        private readonly ILearnRepository _learnRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public LearnCommandHandler(
            ILearnRepository learnRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _learnRepository = learnRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(EnrollStudentDto request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if student exists
                var student = await _studentRepository.GetById(request.StudentId);
                if (student == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Student not found"
                    };
                }

                // Check if course exists
                var course = await _courseRepository.GetById(request.CourseId);
                if (course == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Course not found"
                    };
                }

                // Check if already enrolled
                var isAlreadyEnrolled = await _learnRepository.IsStudentEnrolledInCourseAsync(request.StudentId, request.CourseId);
                if (isAlreadyEnrolled)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Student is already enrolled in this course"
                    };
                }

                var enrollment = _mapper.Map<LearnEntity>(request);
                await _learnRepository.Create(enrollment);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Student enrolled successfully",
                    Data = enrollment
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error enrolling student: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(UnenrollStudentDto request, CancellationToken cancellationToken)
        {
            try
            {
                var enrollment = await _learnRepository.GetLearnByStudentAndCourseAsync(request.StudentId, request.CourseId);
                if (enrollment == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Enrollment not found"
                    };
                }

                await _learnRepository.Delete(enrollment);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Student unenrolled successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error unenrolling student: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(UpdateGradeDto request, CancellationToken cancellationToken)
        {
            try
            {
                var enrollment = await _learnRepository.GetLearnByStudentAndCourseAsync(request.StudentId, request.CourseId);
                if (enrollment == null)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Enrollment not found"
                    };
                }

                enrollment.Grade = request.Grade;
                await _learnRepository.Update(enrollment);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Grade updated successfully",
                    Data = enrollment
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error updating grade: {ex.Message}"
                };
            }
        }
    }
}
