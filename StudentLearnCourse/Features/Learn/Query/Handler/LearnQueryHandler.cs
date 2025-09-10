namespace CRUD_Operation.Features.Learn.Query.Handler
{
    public class LearnQueryHandler : IRequestHandler<GetAllEnrollmentsDto, Response>,
                                     IRequestHandler<GetEnrollmentsByStudentDto, Response>,
                                     IRequestHandler<GetEnrollmentsByCourseDto, Response>
    {
        private readonly ILearnRepository _learnRepository;
        private readonly IMapper _mapper;

        public LearnQueryHandler(ILearnRepository learnRepository, IMapper mapper)
        {
            _learnRepository = learnRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllEnrollmentsDto request, CancellationToken cancellationToken)
        {
            try
            {
                var enrollments = await _learnRepository.GetLearnsWithDetailsAsync();
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Enrollments retrieved successfully",
                    Data = enrollments
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving enrollments: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(GetEnrollmentsByStudentDto request, CancellationToken cancellationToken)
        {
            try
            {
                var enrollments = await _learnRepository.GetLearnsByStudentIdAsync(request.StudentId);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Student enrollments retrieved successfully",
                    Data = enrollments
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving student enrollments: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(GetEnrollmentsByCourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var enrollments = await _learnRepository.GetLearnsByCourseIdAsync(request.CourseId);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Course enrollments retrieved successfully",
                    Data = enrollments
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving course enrollments: {ex.Message}"
                };
            }
        }
    }
}
