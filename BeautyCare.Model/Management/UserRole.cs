using BeautyCare.Model.Entity;
using IntraVision.Data;

namespace BeautyCare.Model.Management
{
    public class UserRole : IdentityUserRoleGenericBase<User, Role>, IBeautyCareRepository
    {
    }
}
