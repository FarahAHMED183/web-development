namespace CRUD_Operation.Controllers
{
    [Route(Router.CourseController)]
    [ApiController]
    public class CourseController : BaseController
    {
        [HttpGet(Router.GetAllCourses)]
        public async Task<IActionResult> GetAllCourses()
        {
            var request = new GetAllCoursesDto();
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.GetCourseById)]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var request = new GetCourseByIdDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.CreateCourse)]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut(Router.UpdateCourse)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpDelete(Router.DeleteCourse)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var request = new DeleteCourseDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
