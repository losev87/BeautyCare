using Microsoft.AspNet.Identity.EntityFramework;

namespace IntraVision.Data
{
    public class IdentityUserRoleGenericBase<TUser,TRole> : IdentityUserRole<int>
        where TUser : class
        where TRole : class
    {
        public virtual TUser User { get; set; }
        public virtual TRole Role { get; set; }
    }
}
