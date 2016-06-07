using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace BL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store)
            : base(store)
        {

        }
    }
}
