using System.Text.Json.Serialization;

namespace CRUD_Operation.Models
{
    public class StudentEntity : BaseEntity
    {
        public string SID { get; set; } // Student ID from ERD
        public string Sname { get; set; } // Student Name from ERD
        public int Age { get; set; } // Age from ERD
        
        // Navigation property for many-to-many relationship
        [JsonIgnore]
        public virtual List<LearnEntity> Learns { get; set; } = new List<LearnEntity>();
    }
}
