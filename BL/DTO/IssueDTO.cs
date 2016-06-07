using DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class IssueDTO
    {
        public int Id { get; set; }

        public int ResponsiblePersonId { get; set; }
        public string ResponsiblePersonEmail { get; set; }

        public int OwnerId { get; set; }
        [Required(ErrorMessage = "Error assigning owner to DTO.")]
        public string OwnerEmail { get; set; }

        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Invalid project name entered.")]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters.")]
        public string Description { get; set; }

        public DateTime? DateFiled { get; set; }
        public DateTime? DateSolved { get; set; }

        [Required(ErrorMessage = "Error assigning type to IssueDTO.")]
        public IssueType Type { get; set; }
        [Required(ErrorMessage = "Error assigning state to IssueDTO.")]
        public IssueState State { get; set; }

        public IssueDTO()
        {
            if(DateFiled == null)
            {
                DateFiled = DateTime.Now;
            }
        }
    }
}
