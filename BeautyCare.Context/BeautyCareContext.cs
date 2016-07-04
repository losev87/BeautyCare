using System.Data.Entity;
using BeautyCare.Model.Entity;
using BeautyCare.Model.Management;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.Context
{
    public class BeautyCareContext : IdentityDbContext<User, Role, int, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>
    {
        public BeautyCareContext()
            : base("BeautyCareContext")
        {
        }

        public DbSet<Publication> Publications { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
