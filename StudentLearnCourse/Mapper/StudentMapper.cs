using CRUD_Operation.Features.Student.Query.Models;

namespace CRUD_Operation.Mapper
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentDto, StudentEntity>()
                .ReverseMap();

            CreateMap<UpdateStudentDto, StudentEntity>()
                .ReverseMap();

            CreateMap<StudentEntity, StudentWithCoursesDto>()
                .ForMember(dest => dest.EnrolledCourses, opt => opt.MapFrom(src => 
                    src.Learns.Select(l => new EnrolledCourseDto
                    {
                        Id = l.Course.Id,
                        Code = l.Course.Code,
                        Cname = l.Course.Cname,
                        Hours = l.Course.Hours,
                        Grade = l.Grade
                    })));
        }
    }
}
