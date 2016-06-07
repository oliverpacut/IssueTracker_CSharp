using AutoMapper.QueryableExtensions;
using BL.DTO;
using BL.DTO.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace BL.Queries
{
    public class IssueListQuery : AppQuery<IssueDTO>
    {
        public IssueFilter Filter { get; set; }

        public IssueListQuery(IUnitOfWorkProvider provider) : base(provider)
        {

        }

        protected override IQueryable<IssueDTO> GetQueryable()
        {
            IQueryable<Issue> query = from iss in Context.Issues select iss;

            if (Filter?.DateFiled != null)
            {
                query = query.Where(y => y.DateFiled == Filter.DateFiled);
            }
            if (Filter?.DateSolved != null)
            {
                query = query.Where(y => y.DateSolved == Filter.DateSolved);
            }
            if (Filter?.ProjectId != null)
            {
                query = query.Where(y => y.ProjectId == Filter.ProjectId);
            }
            if (Filter?.OwnerId != null)
            {
                query = query.Where(y => y.OwnerId == Filter.OwnerId);
            }
            if (Filter?.ResponsiblePersonId != null)
            {
                query = query.Where(y => y.ResponsiblePersonId == Filter.ResponsiblePersonId);
            }
            if (!string.IsNullOrEmpty(Filter?.ResponsiblePersonEmail))
            {
                query = query.Where(y => y.ResponsiblePersonEmail == Filter.ResponsiblePersonEmail);
            }
            if (!string.IsNullOrEmpty(Filter?.ProjectName))
            {
                query = query.Where(y => y.ProjectName == Filter.ProjectName);
            }
            if (!string.IsNullOrEmpty(Filter?.OwnerEmail))
            {
                query = query.Where(y => y.OwnerEmail == Filter.OwnerEmail);
            }
            if (!string.IsNullOrEmpty(Filter?.Name))
            {
                query = query.Where(y => y.Name == Filter.Name);
            }
            if (Filter?.Type != null)
            {
                query = query.Where(y => y.Type == Filter.Type);
            }
            if (Filter?.Type != null)
            {
                query = query.Where(y => y.State == Filter.State);
            }
    
            return query.ProjectTo<IssueDTO>();
        }
    }
}
