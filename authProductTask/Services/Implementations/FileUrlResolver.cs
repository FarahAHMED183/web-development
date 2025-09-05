using Microsoft.AspNetCore.Http;
using CRUD_Operations.Services.Interfaces;

namespace CRUD_Operations.Services.Implementations
{
    public class FileUrlResolver : IFileUrlResolver
    {
        private readonly IHttpContextAccessor _http;

        public FileUrlResolver(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string? Resolve(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return null;
            var req = _http.HttpContext?.Request;
            if (req == null) return relativePath;
            var baseUrl = $"{req.Scheme}://{req.Host}";
            if (!relativePath.StartsWith("/")) relativePath = "/" + relativePath;
            return baseUrl + relativePath;
        }
    }
}
