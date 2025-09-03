using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;

namespace WebApplication2.Mapping
{
    public class ProjectEmployeeProfile : Profile
    {
        public ProjectEmployeeProfile()
        {
            CreateMap<ProjectEmployeeDto, ProjectEmployee>();

            CreateMap<ProjectEmployee, ProjectEmployeeRead>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
        }
    }
}