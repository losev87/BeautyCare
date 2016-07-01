using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IntraVision.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace IntraVision.Web.Mvc.Services
{
    public class UserManagerBase<TUser, TRole, TUserLogin, TUserRole, TUserClaim> : UserManager<TUser, int>
        where TUser : IdentityUser<int, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<int, TUserRole>
        where TUserLogin : IdentityUserLoginBase, new()
        where TUserRole : IdentityUserRoleGenericBase<TUser, TRole>, new()
        where TUserClaim : IdentityUserClaimBase, new()
    {
        protected Lazy<IAuthenticationManager> _authenticationManager;

        public UserManagerBase(IUserStore<TUser, int> store, Lazy<IAuthenticationManager> authenticationManager)
            : base(store)
        {
            _authenticationManager = authenticationManager;

            UserValidator = new UserValidator<TUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 10,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            var provider = new DpapiDataProtectionProvider("UserManager");
            UserTokenProvider = new DataProtectorTokenProvider<TUser, int>(provider.Create("ASP.NET Identity"));
        }

        public virtual async Task SignInAsync(TUser user, bool isPersistent)
        {

            _authenticationManager.Value.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.Value.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
        }

        public void SignIn(params ClaimsIdentity[] parametrs)
        {
            _authenticationManager.Value.SignIn(parametrs);
        }

        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] parametrs)
        {
            _authenticationManager.Value.SignIn(properties, parametrs);
        }

        public void SignOut(string[] authenticationTypes)
        {
            _authenticationManager.Value.SignOut(authenticationTypes);
        }

        public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes()
        {
            return _authenticationManager.Value.GetExternalAuthenticationTypes();
        }
    }
}
