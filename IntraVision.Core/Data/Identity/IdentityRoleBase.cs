using Microsoft.AspNet.Identity.EntityFramework;

namespace IntraVision.Data
{
    public class IdentityRoleBase : IdentityRole<int, IdentityUserRoleBase>, IEntityBase
    {
    }
}
