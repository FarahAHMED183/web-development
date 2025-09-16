using CRUD_Operation.Models;

namespace CRUD_Operation.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<CourseEntity>
    {
        Task<List<CourseEntity>> GetCoursesWithStudentsAsync();
        Task<CourseEntity?> GetCourseWithStudentsByIdAsync(int id);
        Task<PaginatedResult<CourseEntity>> GetCoursesWithStudentsPagedAsync(int pageNumber, int pageSize);
    }

    public interface IStudentRepository : IGenericRepository<StudentEntity>
    {
        Task<List<StudentEntity>> GetStudentsWithCoursesAsync();
        Task<StudentEntity?> GetStudentWithCoursesByIdAsync(int id);
        Task<PaginatedResult<StudentEntity>> GetStudentsWithCoursesPagedAsync(int pageNumber, int pageSize);
    }

    public interface ILearnRepository : IGenericRepository<LearnEntity>
    {
        Task<List<LearnEntity>> GetLearnsWithDetailsAsync();
        Task<List<LearnEntity>> GetLearnsByStudentIdAsync(int studentId);
        Task<List<LearnEntity>> GetLearnsByCourseIdAsync(int courseId);
        Task<LearnEntity?> GetLearnByStudentAndCourseAsync(int studentId, int courseId);
        Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId);
        Task<PaginatedResult<LearnEntity>> GetLearnsWithDetailsPagedAsync(int pageNumber, int pageSize);
    }
}
