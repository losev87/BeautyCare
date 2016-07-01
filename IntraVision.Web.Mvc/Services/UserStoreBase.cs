using System.Data.Entity;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IntraVision.Web.Mvc.Services
{
    public class UserStoreBase<TDbContext, TUser, TRole, TUserLogin, TUserRole, TUserClaim> : UserStore<TUser, TRole, int, TUserLogin, TUserRole, TUserClaim>
        where TUser : IdentityUser<int,TUserLogin,TUserRole,TUserClaim>
	    where TRole : IdentityRole<int,TUserRole>
        where TUserLogin : IdentityUserLoginBase, new()
        where TUserRole : IdentityUserRoleGenericBase<TUser, TRole>, new()
        where TUserClaim : IdentityUserClaimBase, new()
        where TDbContext : DbContext
    {
        public UserStoreBase(TDbContext context)
            : base(context)
        {
        }
    }
}
