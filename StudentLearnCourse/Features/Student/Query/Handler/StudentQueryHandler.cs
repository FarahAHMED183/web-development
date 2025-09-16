using CRUD_Operation.Features.Student.Query.Models;

namespace CRUD_Operation.Features.Student.Query.Handler
{
    public class StudentQueryHandler : IRequestHandler<GetAllStudentsDto, Response>,
                                       IRequestHandler<GetStudentByIdDto, Response>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllStudentsDto request, CancellationToken cancellationToken)
        {
            var studentsResult = await _studentRepository.GetStudentsWithCoursesPagedAsync(request.PageNumber, request.PageSize);
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
                Message = "Students retrieved successfully",
                Data = paginatedResult
            };
        }

        public async Task<Response> Handle(GetStudentByIdDto request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentWithCoursesByIdAsync(request.Id);
            if (student == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Student not found"
                };
            }

            var studentDto = _mapper.Map<StudentWithCoursesDto>(student);

            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Student retrieved successfully",
                Data = studentDto
            };
        }
    }
}
