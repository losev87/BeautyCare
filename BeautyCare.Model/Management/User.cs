using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.Model.Management
{
    public class User : IdentityUser<int, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>, IEntityBase, IManagementRepository
    {
    }
}
