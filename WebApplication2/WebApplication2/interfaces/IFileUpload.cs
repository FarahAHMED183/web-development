namespace WebApplication2.Interfaces
{
    public interface IFileUpload
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        void DeleteFile(string filePath);
    }
}