namespace CRUD_Operation.Repositories.Interfaces
{
    public interface IStudentRepository : IGenericRepository<StudentEntity>
    {
        Task<List<StudentEntity>> GetStudentsWithCoursesAsync();
        Task<StudentEntity?> GetStudentWithCoursesByIdAsync(int id);
        Task<List<StudentEntity>> GetStudentsByCourseIdAsync(int courseId);
    }
}
