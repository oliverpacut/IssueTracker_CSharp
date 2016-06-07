using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.DTO;
using BL.DTO.Filters;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class CommentListQuery : AppQuery<CommentDTO>
    {
        public CommentFilter Filter { get; set; }

        public CommentListQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        protected override IQueryable<CommentDTO> GetQueryable()
        {
            var query = from comm in Context.Comments select comm;

            if (Filter?.CreationTime != null)
            {
                query = query.Where(y => y.CreationTime == Filter.CreationTime);
            }
            if (Filter?.IssueId != null)
            {
                query = query.Where(y => y.IssueId == Filter.IssueId);
            }
            if (Filter?.OwnerId != null)
            {
                query = query.Where(y => y.OwnerId == Filter.OwnerId);
            }

            return query.ProjectTo<CommentDTO>();
        }
    }
}
