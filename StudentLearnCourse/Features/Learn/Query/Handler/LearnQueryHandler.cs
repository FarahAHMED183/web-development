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
            var enrollmentsResult = await _learnRepository.GetLearnsWithDetailsPagedAsync(request.PageNumber, request.PageSize);
            
            var paginatedResult = new PaginatedResult<LearnEntity>(
                enrollmentsResult.Items,
                enrollmentsResult.PageNumber,
                enrollmentsResult.PageSize,
                enrollmentsResult.TotalRecords,
                enrollmentsResult.TotalPages);
            
            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Enrollments retrieved successfully",
                Data = paginatedResult
            };
        }

        public async Task<Response> Handle(GetEnrollmentsByStudentDto request, CancellationToken cancellationToken)
        {
            var enrollments = await _learnRepository.GetLearnsByStudentIdAsync(request.StudentId);
            
            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Student enrollments retrieved successfully",
                Data = enrollments
            };
        }

        public async Task<Response> Handle(GetEnrollmentsByCourseDto request, CancellationToken cancellationToken)
        {
            var enrollments = await _learnRepository.GetLearnsByCourseIdAsync(request.CourseId);
            
            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Course enrollments retrieved successfully",
                Data = enrollments
            };
        }
    }
}
