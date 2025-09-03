using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;

namespace WebApplication2.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<Project, ProjectReadDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

        }
    }
}