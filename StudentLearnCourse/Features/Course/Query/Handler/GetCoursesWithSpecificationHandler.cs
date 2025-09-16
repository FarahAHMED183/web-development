using CRUD_Operation.Features.Course.Query.Models;
using CRUD_Operation.Repositories.Interfaces;

namespace CRUD_Operation.Features.Course.Query.Handler;

// Example: Enhanced query handler using specifications
public class GetCoursesWithSpecificationQuery : IRequest<Response>
{
    public string? CourseName { get; set; }
    public int MinHours { get; set; }
    public string? CodePrefix { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}

public class GetCoursesWithSpecificationHandler : IRequestHandler<GetCoursesWithSpecificationQuery, Response>
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesWithSpecificationHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Response> Handle(GetCoursesWithSpecificationQuery request, CancellationToken cancellationToken)
    {
        // Create specification based on request parameters
        var specification = new CourseSpecification();

        // Add criteria dynamically based on request
        if (!string.IsNullOrEmpty(request.CourseName))
            specification.AddCriteria(c => c.Cname.Contains(request.CourseName));

        if (request.MinHours > 0)
            specification.AddCriteria(c => c.Hours >= request.MinHours);

        if (!string.IsNullOrEmpty(request.CodePrefix))
            specification.AddCriteria(c => c.Code.StartsWith(request.CodePrefix));

        // Apply pagination
        if (request.Take > 0)
            specification.ApplyPagination(request.Skip, request.Take);

        // Execute query using specification
        var courses = await _courseRepository.GetBySpecification(specification);
        var totalCount = await _courseRepository.CountBySpecification(specification);

        return new Response
        {
            Status = true,
            StatusCode = HttpStatusCode.OK,
            Message = "Courses retrieved successfully using specifications",
            Data = new 
            {
                TotalCount = totalCount,
                CurrentPage = courses.Count,
                Items = courses
            }
        };
    }
}
