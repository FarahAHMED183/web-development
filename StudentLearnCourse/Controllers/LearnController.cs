namespace CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var request = new GetAllEnrollmentsDto();
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetEnrollmentsByStudent(int studentId)
        {
            var request = new GetEnrollmentsByStudentDto { StudentId = studentId };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetEnrollmentsByCourse(int courseId)
        {
            var request = new GetEnrollmentsByCourseDto { CourseId = courseId };
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPost("unenroll")]
        public async Task<IActionResult> UnenrollStudent([FromBody] UnenrollStudentDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }

        [HttpPut("grade")]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeDto request)
        {
            var response = await mediator.Send(request);
            return Result(response);
        }
    }
}
