namespace CRUD_Operation.Controllers
{
    [ApiController]
    public class CourseController : BaseController
    {
        [HttpGet(Router.CourseRouter.GetAll)]
        public async Task<IActionResult> GetAllCourses([FromQuery] GetAllCoursesDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.CourseRouter.GetById)]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var request = new GetCourseByIdDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.CourseRouter.Create)]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut(Router.CourseRouter.Update)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpDelete(Router.CourseRouter.Delete)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var request = new DeleteCourseDto { Id = id };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.CourseRouter.Search)]
        public async Task<IActionResult> SearchCourses(
            [FromQuery] string? name = null,
            [FromQuery] int? hours = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var request = new SearchCoursesRequest
            {
                Name = name,
                Hours = hours,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
