using CRUD_Operation.Models;
using CRUD_Operation.Specifications.Base;
using CRUD_Operation.Features.Student.Command.Models;

namespace CRUD_Operation.Specifications;

public class StudentSpecification : BaseSpecification<StudentEntity>
{
    public StudentSpecification(StudentDto? studentDto = null) 
        : base()
    {
        if (studentDto != null)
        {
            if (!string.IsNullOrEmpty(studentDto.Sname))
            {
                AddCriteria(x => x.Sname.Contains(studentDto.Sname));
            }
            if (!string.IsNullOrEmpty(studentDto.SID))
            {
                AddCriteria(x => x.SID == studentDto.SID);
            }
            if (studentDto.Age > 0)
            {
                AddCriteria(x => x.Age == studentDto.Age);
            }
        }

        AddInclude(x => x.Learns); 
        ApplyOrderByAsc(x => x.Sname);
    }
}
