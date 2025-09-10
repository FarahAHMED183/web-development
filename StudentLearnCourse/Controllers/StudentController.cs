namespace CRUD_Operation.Controllers
{
    [Route(Router.StudentController)]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet(Router.GetAllStudents)]
        public async Task<IActionResult> GetAllStudents()
        {
            var request = new GetAllStudentsDto();
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.GetStudentById)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var request = new GetStudentByIdDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.CreateStudent)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut(Router.UpdateStudent)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpDelete(Router.DeleteStudent)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var request = new DeleteStudentDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.SearchStudents)]
        public async Task<IActionResult> SearchStudents([FromQuery] string? name = null, [FromQuery] int age = 0)
        {
            var studentDto = new StudentDto
            {
                Sname = name ?? "",
                Age = age
            };

            var specification = new StudentSpecification(studentDto);
            var students = await _studentRepository.GetBySpecification(specification);

            var response = new Response
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"Found {students.Count} students matching criteria",
                Data = students.Select(s => new
                {
                    s.Id,
                    s.SID,
                    s.Sname,
                    s.Age,
                    EnrolledCourses = s.Learns?.Count ?? 0
                })
            };

            return Result(response);
        }
    }
}
