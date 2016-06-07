using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Error assigning PersonID to IssueDTO.")]
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Error assigning IssueID to IssueDTO.")]
        public int IssueId { get; set; }

        [Required(ErrorMessage = "Comment body is empty.")]
        public string Body { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
