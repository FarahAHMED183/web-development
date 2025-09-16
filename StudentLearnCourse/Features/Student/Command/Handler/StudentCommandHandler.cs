namespace CRUD_Operation.Features.Student.Command.Handler
{
    public class StudentCommandHandler : IRequestHandler<StudentDto, Response>,
                                         IRequestHandler<UpdateStudentDto, Response>,
                                         IRequestHandler<DeleteStudentDto, Response>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentCommandHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(StudentDto request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<StudentEntity>(request);
            await _studentRepository.Create(student);
            
            return new Response
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Student created successfully",
                Data = student
            };
        }

        public async Task<Response> Handle(UpdateStudentDto request, CancellationToken cancellationToken)
        {
            var existingStudent = await _studentRepository.GetById(request.Id);
            if (existingStudent == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Student not found"
                };
            }

            _mapper.Map(request, existingStudent);
            await _studentRepository.Update(existingStudent);
            
            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Student updated successfully",
                Data = existingStudent
            };
        }

        public async Task<Response> Handle(DeleteStudentDto request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetById(request.Id);
            if (student == null)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Student not found"
                };
            }

            await _studentRepository.Delete(student);
            
            return new Response
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Student deleted successfully"
            };
        }
    }
}
