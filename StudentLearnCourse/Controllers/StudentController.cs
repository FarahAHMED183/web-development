namespace CRUD_Operation.Controllers
{
    [ApiController]
    public class StudentController : BaseController
    {
        [HttpGet(Router.StudentRouter.GetAllStudents)]
        public async Task<IActionResult> GetAllStudents([FromQuery] GetAllStudentsDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.StudentRouter.GetStudentById)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var request = new GetStudentByIdDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.StudentRouter.CreateStudent)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut(Router.StudentRouter.UpdateStudent)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpDelete(Router.StudentRouter.DeleteStudent)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var request = new DeleteStudentDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.StudentRouter.SearchStudents)]
        public async Task<IActionResult> SearchStudents([FromQuery] string? name = null, [FromQuery] int age = 0, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new SearchStudentsRequest
            {
                Name = name,
                Age = age,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
