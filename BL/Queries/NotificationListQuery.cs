using AutoMapper.QueryableExtensions;
using BL.DTO;
using BL.DTO.Filters;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace BL.Queries
{
    public  class NotificationListQuery : AppQuery<NotificationDTO>
    {
        public NotificationFilter Filter { get; set; }

        public NotificationListQuery(IUnitOfWorkProvider provider) : base(provider)
        {

        }

        protected override IQueryable<NotificationDTO> GetQueryable()
        {
            var query = from notif in Context.Notifications select notif;

            if (Filter?.Creationtime != null)
            {
                query = query.Where(y => y.CreationTime == y.CreationTime);
            }
            if(Filter?.PersonRecipient != null)
            {
                query = query.Where(y => y.PersonRecipientId == Filter.PersonRecipient);
            }
            if(Filter?.Seen != null)
            {
                query = query.Where(y => y.Seen == Filter.Seen);
            }

            return query.ProjectTo<NotificationDTO>();
        }
    }
}
