using CRUD_Operation.Models;
using CRUD_Operation.Repositories.Interfaces;

namespace CRUD_Operation.Repositories.Implementations
{
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<CourseEntity>> GetCoursesWithStudentsAsync()
        {
            return await _context.Courses.Include(c => c.Learns).ThenInclude(l => l.Student).ToListAsync();
        }

        public async Task<CourseEntity?> GetCourseWithStudentsByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Learns).ThenInclude(l => l.Student).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PaginatedResult<CourseEntity>> GetCoursesWithStudentsPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses.Include(c => c.Learns).ThenInclude(l => l.Student);
            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<CourseEntity>(items, pageNumber, pageSize, totalRecords, totalPages);
        }
    }

    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<StudentEntity>> GetStudentsWithCoursesAsync()
        {
            return await _context.Students.Include(s => s.Learns).ThenInclude(l => l.Course).ToListAsync();
        }

        public async Task<StudentEntity?> GetStudentWithCoursesByIdAsync(int id)
        {
            return await _context.Students.Include(s => s.Learns).ThenInclude(l => l.Course).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<PaginatedResult<StudentEntity>> GetStudentsWithCoursesPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Students.Include(s => s.Learns).ThenInclude(l => l.Course);
            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<StudentEntity>(items, pageNumber, pageSize, totalRecords, totalPages);
        }
    }

    public class LearnRepository : GenericRepository<LearnEntity>, ILearnRepository
    {
        public LearnRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<LearnEntity>> GetLearnsWithDetailsAsync()
        {
            return await _context.Learns.Include(l => l.Student).Include(l => l.Course).ToListAsync();
        }

        public async Task<List<LearnEntity>> GetLearnsByStudentIdAsync(int studentId)
        {
            return await _context.Learns.Include(l => l.Course).Where(l => l.StudentId == studentId).ToListAsync();
        }

        public async Task<List<LearnEntity>> GetLearnsByCourseIdAsync(int courseId)
        {
            return await _context.Learns.Include(l => l.Student).Where(l => l.CourseId == courseId).ToListAsync();
        }

        public async Task<LearnEntity?> GetLearnByStudentAndCourseAsync(int studentId, int courseId)
        {
            return await _context.Learns.FirstOrDefaultAsync(l => l.StudentId == studentId && l.CourseId == courseId);
        }

        public async Task<bool> IsStudentEnrolledInCourseAsync(int studentId, int courseId)
        {
            return await _context.Learns.AnyAsync(l => l.StudentId == studentId && l.CourseId == courseId);
        }

        public async Task<PaginatedResult<LearnEntity>> GetLearnsWithDetailsPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Learns.Include(l => l.Student).Include(l => l.Course);
            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<LearnEntity>(items, pageNumber, pageSize, totalRecords, totalPages);
        }
    }
}
