using AutoMapper.QueryableExtensions;
using BL.DTO;
using BL.DTO.Filters;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace BL.Queries
{
    public class ProjectListQuery : AppQuery<ProjectDTO>
    {
        public ProjectFilter Filter { get; set; }

        public ProjectListQuery(IUnitOfWorkProvider provider) : base(provider)
        {

        }

        protected override IQueryable<ProjectDTO> GetQueryable()
        {
            var query = from proj in Context.Projects select proj;
            
            if (!string.IsNullOrEmpty(Filter?.Name))
            {
                query = query.Where(y => y.Name == Filter.Name);
            }
            if (!string.IsNullOrEmpty(Filter?.Owner))
            {
                query = query.Where(y => y.OwnerEmail == Filter.Owner);
            }
                
            return query.ProjectTo<ProjectDTO>();
        }
    }
}
