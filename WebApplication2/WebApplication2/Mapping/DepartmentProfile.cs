using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;

namespace WebApplication2.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentCreateDto, Department>();
        }
    }
}