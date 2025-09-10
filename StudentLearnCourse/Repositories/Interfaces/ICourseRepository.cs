namespace CRUD_Operation.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<CourseEntity>
    {
        Task<List<CourseEntity>> GetCoursesWithStudentsAsync();
        Task<CourseEntity?> GetCourseWithStudentsByIdAsync(int id);
        Task<List<CourseEntity>> GetCoursesByStudentIdAsync(int studentId);
    }
}
