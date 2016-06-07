using DAL.Enums;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Issue : IEntity<int>
    {
        public int Id { get; set; }

        public int ResponsiblePersonId { get; set; }
        public string ResponsiblePersonEmail { get; set; }

        public int OwnerId { get; set; }
        public string OwnerEmail { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }


        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime DateFiled { get; set; }
        public DateTime? DateSolved { get; set; }
        
        public IssueType Type { get; set; }
        public IssueState State { get; set; }
    }
}
