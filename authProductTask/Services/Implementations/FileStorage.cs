using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using CRUD_Operations.Services.Interfaces;

namespace CRUD_Operations.Services.Implementations
{
    public class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;

        public FileStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveAsync(IFormFile? file, string subFolder)
        {
            if (file == null || file.Length == 0) return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot"), subFolder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var relativePath = Path.Combine(subFolder, fileName).Replace("\\", "/");
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return relativePath;
        }
    }
}
