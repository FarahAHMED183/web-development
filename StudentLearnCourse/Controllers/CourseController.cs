namespace CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var request = new GetAllCoursesDto();
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var request = new GetCourseByIdDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var request = new DeleteCourseDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
