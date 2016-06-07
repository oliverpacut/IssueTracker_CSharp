using DAL.Enums;
using System;

namespace BL.DTO.Filters
{
    public class IssueFilter
    {
        public int ResponsiblePersonId { get; set; }
        public string ResponsiblePersonEmail { get; set; }

        public int? OwnerId { get; set; }
        public string OwnerEmail { get; set; }

        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string Name { get; set; }

        public DateTime? DateFiled { get; set; }
        public DateTime? DateSolved { get; set; }

        public IssueType? Type { get; set; }
        public IssueState? State { get; set; }
    }
}
