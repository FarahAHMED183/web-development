using CRUD_Operation.Features.Course.Query.Models;

namespace CRUD_Operation.Mapper
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<CourseDto, CourseEntity>()
                .ReverseMap();

            CreateMap<UpdateCourseDto, CourseEntity>()
                .ReverseMap();

            CreateMap<CourseEntity, CourseWithStudentsDto>()
                .ForMember(dest => dest.EnrolledStudents, opt => opt.MapFrom(src => 
                    src.Learns.Select(l => new EnrolledStudentDto
                    {
                        Id = l.Student.Id,
                        SID = l.Student.SID,
                        Sname = l.Student.Sname,
                        Age = l.Student.Age,
                        Grade = l.Grade
                    })));
        }
    }
}
