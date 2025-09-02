using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;

namespace WebApplication2.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<EmployeeCreateDto, Employee>();
        }
    }
}