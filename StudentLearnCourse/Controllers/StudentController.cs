namespace CRUD_Operation.Controllers
{
    [Route(Router.StudentController)]
    [ApiController]
    public class StudentController : BaseController
    {
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
    }
}
