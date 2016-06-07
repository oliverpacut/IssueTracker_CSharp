using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        public int IssueId { get; set; }

        public string Body { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
