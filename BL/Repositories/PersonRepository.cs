using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class PersonRepository : EntityFrameworkRepository<Person, int>
    {
        public PersonRepository(IUnitOfWorkProvider provider) : base(provider)
        {

        }
    }
}
