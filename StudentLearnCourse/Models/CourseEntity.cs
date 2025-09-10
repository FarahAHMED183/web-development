using System.Text.Json.Serialization;

namespace CRUD_Operation.Models
{
    public class CourseEntity : BaseEntity
    {
        public string Code { get; set; } 
        public string Cname { get; set; } 
        public int Hours { get; set; } 
        
        // Navigation property for many-to-many relationship
        [JsonIgnore]
        public virtual List<LearnEntity> Learns { get; set; } = new List<LearnEntity>();
    }
}
