using BL.DTO;
using BL.DTO.Filters;
using System.Linq;
using Riganti.Utils.Infrastructure.Core;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class PersonListQuery : AppQuery<PersonDTO>
    {
        public PersonFilter Filter { get; set; }
        public PersonListQuery(IUnitOfWorkProvider provider) : base(provider)
        {

        }

        protected override IQueryable<PersonDTO> GetQueryable()
        {
            var query = from pers in Context.People select pers;

            if (!string.IsNullOrEmpty(Filter?.Email))
            {
                query = query.Where(y => y.Email == Filter.Email);
            }
            if(Filter?.IsAdmin != null)
            {
                query = query.Where(y => y.IsAdmin == Filter.IsAdmin);
            }
            if (!string.IsNullOrEmpty(Filter?.Name))
            {
                query = query.Where(y => y.Name == Filter.Name);
            }

            return query.ProjectTo<PersonDTO>();
        }
    }
}
