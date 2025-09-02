using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dto;

namespace WebApplication2.Mapping
{
    public class DependentProfile : Profile
    {
        public DependentProfile()
        {
            CreateMap<Dependent, DependentDto>();
        }
    }
}