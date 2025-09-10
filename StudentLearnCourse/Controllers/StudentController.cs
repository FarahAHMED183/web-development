namespace CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var request = new GetAllStudentsDto();
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var request = new GetStudentByIdDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var request = new DeleteStudentDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
