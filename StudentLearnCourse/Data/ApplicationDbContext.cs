namespace CRUD_Operation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        // Student-Course-Learn relationship entities
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<LearnEntity> Learns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student-Course relationship (many-to-many through Learn entity)
            modelBuilder.Entity<LearnEntity>()
                .HasOne<StudentEntity>(l => l.Student)
                .WithMany(s => s.Learns)
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // When Student is deleted, delete related enrollments

            modelBuilder.Entity<LearnEntity>()
                .HasOne<CourseEntity>(l => l.Course)
                .WithMany(c => c.Learns)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // When Course is deleted, delete related enrollments

            // Ensure unique combination of StudentId and CourseId
            modelBuilder.Entity<LearnEntity>()
                .HasIndex(l => new { l.StudentId, l.CourseId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);  
        }
    }
}
