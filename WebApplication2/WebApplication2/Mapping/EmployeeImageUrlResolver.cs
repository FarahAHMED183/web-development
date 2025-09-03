using AutoMapper;
using Microsoft.AspNetCore.Http;
using WebApplication2.Models;
using WebApplication2.Dto;

public class EmployeeImageUrlResolver : IValueResolver<Employee, EmployeeDto, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeeImageUrlResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Resolve(Employee source, EmployeeDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ImagePath))
            return string.Empty;

        var request = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";
        return $"{baseUrl}/employee/{source.ImagePath}";
    }
}