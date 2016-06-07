using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }

        [Required]
        public int PersonRecipientId { get; set; }

        public string Body { get; set; }

        public bool Seen { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
