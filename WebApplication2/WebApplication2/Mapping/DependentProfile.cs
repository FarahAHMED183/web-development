using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;

namespace WebApplication2.Mapping
{
    public class DependentProfile : Profile
    {
        public DependentProfile()
        {
            CreateMap<DependentCreateDto, Dependent>();
            CreateMap<Dependent, DependentReadDto>() 
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
        }
    }
}
