using System.Data.Entity;
using BeautyCare.Model.Management;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.ContextManagement
{
    public class ManagementContext : IdentityDbContext<User, Role, int, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>
    {
        public ManagementContext()
            : base("ManagementContext")
        {
        }

        public DbSet<Permission> Permissions { get; set; }
    }
}
