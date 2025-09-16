namespace CRUD_Operation.Controllers
{
    [ApiController]
    public class LearnController : BaseController
    {
        [HttpGet(Router.LearnRouter.GetAllEnrollments)]
        public async Task<IActionResult> GetAllEnrollments([FromQuery] GetAllEnrollmentsDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.LearnRouter.GetEnrollmentsByStudent)]
        public async Task<IActionResult> GetEnrollmentsByStudent(int studentId)
        {
            var request = new GetEnrollmentsByStudentDto { StudentId = studentId };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.LearnRouter.GetEnrollmentsByCourse)]
        public async Task<IActionResult> GetEnrollmentsByCourse(int courseId)
        {
            var request = new GetEnrollmentsByCourseDto { CourseId = courseId };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.LearnRouter.EnrollStudent)]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.LearnRouter.UnenrollStudent)]
        public async Task<IActionResult> UnenrollStudent([FromBody] UnenrollStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut(Router.LearnRouter.UpdateGrade)]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
