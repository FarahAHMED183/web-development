using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations.Dtos
{
    public class ChangePasswordModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
