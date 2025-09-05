using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CRUD_Operations.Services.Interfaces
{
    public interface IFileStorage
    {
        Task<string?> SaveAsync(IFormFile? file, string subFolder);
    }
}
