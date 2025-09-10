using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Models
{
    public class LearnEntity
    {
        [Key]
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public virtual StudentEntity Student { get; set; }
        
        public int CourseId { get; set; }
        public virtual CourseEntity Course { get; set; }
        
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public string Grade { get; set; } 
    }
}
