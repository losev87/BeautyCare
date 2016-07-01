using System;
using IntraVision.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace IntraVision.Web.Mvc.Services
{
    public class RoleManagerBase<TUser, TRole, TUserLogin, TUserRole, TUserClaim> : RoleManager<TRole, int>
        where TUser : IdentityUser<int, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<int, TUserRole>
        where TUserLogin : IdentityUserLoginBase, new()
        where TUserRole : IdentityUserRoleGenericBase<TUser, TRole>, new()
        where TUserClaim : IdentityUserClaimBase, new()
    {
        private Lazy<IAuthenticationManager> _authenticationManager;

        public RoleManagerBase(IRoleStore<TRole, int> store)
            : base(store)
        {
        }
    }
}
