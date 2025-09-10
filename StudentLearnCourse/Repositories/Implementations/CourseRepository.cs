namespace CRUD_Operation.Repositories.Implementations
{
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<CourseEntity>> GetCoursesWithStudentsAsync()
        {
            return await _context.Courses
                .Include(c => c.Learns)
                .ThenInclude(l => l.Student)
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetCourseWithStudentsByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Learns)
                .ThenInclude(l => l.Student)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CourseEntity>> GetCoursesByStudentIdAsync(int studentId)
        {
            return await _context.Courses
                .Where(c => c.Learns.Any(l => l.StudentId == studentId))
                .Include(c => c.Learns)
                .ThenInclude(l => l.Student)
                .ToListAsync();
        }
    }
}
