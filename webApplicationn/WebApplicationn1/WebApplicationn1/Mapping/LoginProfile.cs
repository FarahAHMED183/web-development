using AutoMapper;
using WebApplicationn1.Dto;
using WebApplicationn1.Models;

namespace WebApplicationn1.Mapping
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<Login, LoginDto>().ReverseMap();
        }
    }
}