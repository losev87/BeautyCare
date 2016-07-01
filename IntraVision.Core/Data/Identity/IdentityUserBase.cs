using Microsoft.AspNet.Identity.EntityFramework;

namespace IntraVision.Data
{
    public class IdentityUserBase : IdentityUser<int, IdentityUserLoginBase, IdentityUserRoleBase, IdentityUserClaimBase>, IEntityBase
    {
    }
}
