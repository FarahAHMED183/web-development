using AutoMapper;
using WebApplicationn1.Dto;
using WebApplicationn1.Models;

namespace WebApplicationn1.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmolyeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Login.Username));

            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
        }
    }
}