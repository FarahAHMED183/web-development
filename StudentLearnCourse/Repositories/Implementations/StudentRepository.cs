namespace CRUD_Operation.Repositories.Implementations
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<StudentEntity>> GetStudentsWithCoursesAsync()
        {
            return await _context.Students
                .Include(s => s.Learns)
                .ThenInclude(l => l.Course)
                .ToListAsync();
        }

        public async Task<StudentEntity?> GetStudentWithCoursesByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Learns)
                .ThenInclude(l => l.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<StudentEntity>> GetStudentsByCourseIdAsync(int courseId)
        {
            return await _context.Students
                .Where(s => s.Learns.Any(l => l.CourseId == courseId))
                .Include(s => s.Learns)
                .ThenInclude(l => l.Course)
                .ToListAsync();
        }
    }
}
