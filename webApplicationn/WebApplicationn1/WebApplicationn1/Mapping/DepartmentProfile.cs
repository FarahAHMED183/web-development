using AutoMapper;
using WebApplicationn1.Dto;
using WebApplicationn1.Models;

namespace WebApplicationn1.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
        }
    }
}