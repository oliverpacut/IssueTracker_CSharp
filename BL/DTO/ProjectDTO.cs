using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters.")]
        public string Description { get; set; }
    }
}
