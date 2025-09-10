using CRUD_Operation.Models;
using CRUD_Operation.Specifications.Base;

namespace CRUD_Operation.Specifications;

public class LearnSpecification : BaseSpecification<LearnEntity>
{
    public LearnSpecification(int? studentId = null, int? courseId = null, string? grade = null) 
        : base()
    {
        if (studentId.HasValue)
        {
            AddCriteria(x => x.StudentId == studentId.Value);
        }
        
        if (courseId.HasValue)
        {
            AddCriteria(x => x.CourseId == courseId.Value);
        }
        
        if (!string.IsNullOrEmpty(grade))
        {
            AddCriteria(x => x.Grade == grade);
        }

        AddInclude(x => x.Student);
        AddInclude(x => x.Course);
        ApplyOrderByAsc(x => x.EnrollmentDate);
    }

    public LearnSpecification(DateTime? enrollmentDate = null) 
        : base()
    {
        if (enrollmentDate.HasValue)
        {
            AddCriteria(x => x.EnrollmentDate.Date == enrollmentDate.Value.Date);
        }

        AddInclude(x => x.Student);
        AddInclude(x => x.Course);
        ApplyOrderByAsc(x => x.EnrollmentDate);
    }
}
