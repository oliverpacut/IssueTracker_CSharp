using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Email is required.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        public string Name { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}
