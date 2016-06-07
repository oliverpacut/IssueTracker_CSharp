using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(IUnitOfWorkProvider unitOfWorkProvider)
           : base((unitOfWorkProvider.GetCurrent() as AppUnitOfWork).Context)
        {
        }
    }
}
