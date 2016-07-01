using System;
using BeautyCare.Model.Management;
using IntraVision.Data;
using IntraVision.Web.Mvc.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BeautyCare.Service
{
    public class UserManager : UserManagerBase<User, Role, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>
    {
        public UserManager(IUserStore<User, int> store, Lazy<IAuthenticationManager> authenticationManager)
            : base(store, authenticationManager)
        {
        }
    }
}
