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
    public class Notification : IEntity<int>
    {
        public int Id { get; set; }

        public int PersonRecipientId { get; set; }

        public string Body { get; set; }

        public bool Seen { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
