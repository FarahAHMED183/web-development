namespace CRUD_Operations.Services.Interfaces
{
    public interface IFileUrlResolver
    {
        string? Resolve(string? relativePath);
    }
}
