namespace CRUD_Operation.Controllers
{
    [Route(Router.LearnController)]
    [ApiController]
    public class LearnController : BaseController
    {
        [HttpGet(Router.GetAllEnrollments)]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var request = new GetAllEnrollmentsDto();
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.GetEnrollmentsByStudent)]
        public async Task<IActionResult> GetEnrollmentsByStudent(int studentId)
        {
            var request = new GetEnrollmentsByStudentDto { StudentId = studentId };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet(Router.GetEnrollmentsByCourse)]
        public async Task<IActionResult> GetEnrollmentsByCourse(int courseId)
        {
            var request = new GetEnrollmentsByCourseDto { CourseId = courseId };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.EnrollStudent)]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost(Router.UnenrollStudent)]
        public async Task<IActionResult> UnenrollStudent([FromBody] UnenrollStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut(Router.UpdateGrade)]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
