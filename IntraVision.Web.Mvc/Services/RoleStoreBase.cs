using System.Data.Entity;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IntraVision.Web.Mvc.Services
{
    public class RoleStoreBase<TDbContext, TUser, TRole, TUserLogin, TUserRole, TUserClaim> : RoleStore<TRole, int, TUserRole>
        where TUser : IdentityUser<int,TUserLogin,TUserRole,TUserClaim>
	    where TRole : IdentityRole<int,TUserRole>, new()
        where TUserLogin : IdentityUserLoginBase, new()
        where TUserRole : IdentityUserRoleGenericBase<TUser, TRole>, new()
        where TUserClaim : IdentityUserClaimBase, new()
        where TDbContext : DbContext
    {
        public RoleStoreBase(TDbContext context)
            : base(context)
        {
        }
    }
}
