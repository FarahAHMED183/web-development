namespace CRUD_Operation.Repositories.Implementations
{
    public class LearnRepository : GenericRepository<LearnEntity>, ILearnRepository
    {
        public LearnRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<LearnEntity>> GetLearnsWithDetailsAsync()
        {
            return await _context.Learns
                .Include(l => l.Student)
                .Include(l => l.Course)
                .ToListAsync();
        }

        public async Task<LearnEntity?> GetLearnByStudentAndCourseAsync(int studentId, int courseId)
        {
            return await _context.Learns
                .Include(l => l.Student)
                .Include(l => l.Course)
                .FirstOrDefaultAsync(l => l.StudentId == studentId && l.CourseId == courseId);
        }

        public async Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId)
        {
            return await _context.Learns
                .AnyAsync(l => l.StudentId == studentId && l.CourseId == courseId);
        }

        public async Task<List<LearnEntity>> GetLearnsByStudentIdAsync(int studentId)
        {
            return await _context.Learns
                .Include(l => l.Student)
                .Include(l => l.Course)
                .Where(l => l.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<LearnEntity>> GetLearnsByCourseIdAsync(int courseId)
        {
            return await _context.Learns
                .Include(l => l.Student)
                .Include(l => l.Course)
                .Where(l => l.CourseId == courseId)
                .ToListAsync();
        }
    }
}
