using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _env;

        public FileUpload(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            string uploadPath = Path.Combine(_env.WebRootPath, folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderName, fileName).Replace("\\", "/");
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}