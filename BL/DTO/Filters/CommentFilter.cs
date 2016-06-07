using System;

namespace BL.DTO.Filters
{
    public class CommentFilter
    {
        public int? OwnerId { get; set; }
        public string Owner { get; set; }

        public int? IssueId { get; set; }

        public DateTime? CreationTime { get; set; }
    }
}
