namespace CRUD_Operation.Repositories.Interfaces
{
    public interface ILearnRepository : IGenericRepository<LearnEntity>
    {
        Task<List<LearnEntity>> GetLearnsWithDetailsAsync();
        Task<LearnEntity?> GetLearnByStudentAndCourseAsync(int studentId, int courseId);
        Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId);
        Task<List<LearnEntity>> GetLearnsByStudentIdAsync(int studentId);
        Task<List<LearnEntity>> GetLearnsByCourseIdAsync(int courseId);
    }
}
