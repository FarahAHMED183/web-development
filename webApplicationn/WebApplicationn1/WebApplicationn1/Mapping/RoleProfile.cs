using AutoMapper;
using WebApplicationn1.Dto;
using WebApplicationn1.Models;

namespace WebApplicationn1.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}