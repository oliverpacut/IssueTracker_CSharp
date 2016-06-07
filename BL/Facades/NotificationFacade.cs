using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System.Collections.Generic;
using System.Linq;
namespace BL.Facades
{
    public class NotificationFacade : AppFacadeBase
    {
        public NotificationRepository Repository { get; set; }

        public NotificationListQuery NotificationListQuery { get; set; }


        protected IQuery<NotificationDTO> CreateQuery(NotificationFilter filter = null)
        {
            var query = NotificationListQuery;
            query.Filter = filter;
            return query;
        }

        public void CreateNotification(NotificationDTO notification)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Insert(Mapper.Map<Notification>(notification));
                unitOfWork.Commit();
            }
        }

        public NotificationDTO GetNotificationById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return Mapper.Map<NotificationDTO>(Repository.GetById(id));
            }
        }

        public List<NotificationDTO> GetAllNotifications()
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(null).Execute().ToList();
            }
        }

        public List<NotificationDTO> GetNotifications(NotificationFilter filter)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(filter).Execute().ToList();
            }
        }

        public void UpdateNotification(NotificationDTO notification)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Update(Mapper.Map(notification, Repository.GetById(notification.Id)));
                unitOfWork.Commit();
            }
        }

        public void MarkAsSeen(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.GetById(id).Seen = true;
                unitOfWork.Commit();
            }
        }

        public void DeleteNotification(NotificationDTO notification)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(Mapper.Map<Notification>(notification));
                unitOfWork.Commit();
            }
        }

        public void DeleteById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(id);
                unitOfWork.Commit();
            }
        }

        public void DeleteByIds(params int[] ids)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                ids.ToList().ForEach(y => Repository.Delete(y));
                unitOfWork.Commit();
            }
        }

        public void DeleteStaleNotifications()
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                GetAllNotifications().Where(y => y.Seen == true)
                    .ToList().ForEach(y => Repository.Delete(y.Id));
                unitOfWork.Commit();
            }
        }
    }
}
