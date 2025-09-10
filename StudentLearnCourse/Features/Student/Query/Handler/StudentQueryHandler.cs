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
            try
            {
                var students = await _studentRepository.GetStudentsWithCoursesAsync();
                var studentsDto = _mapper.Map<List<StudentWithCoursesDto>>(students);
                
                return new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Students retrieved successfully",
                    Data = studentsDto
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving students: {ex.Message}"
                };
            }
        }

        public async Task<Response> Handle(GetStudentByIdDto request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"Error retrieving student: {ex.Message}"
                };
            }
        }
    }
}
