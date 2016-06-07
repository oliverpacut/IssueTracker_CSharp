using System;

namespace BL.DTO.Filters
{
    public class NotificationFilter
    {
        public int? PersonRecipient { get; set; }

        public bool? Seen { get; set; }

        public DateTime? Creationtime { get; set; }
    }
}
