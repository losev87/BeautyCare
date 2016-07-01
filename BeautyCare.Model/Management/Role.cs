using System.Collections.Generic;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.Model.Management
{
    public class Role : IdentityRole<int, UserRole>, IEntityBase, IManagementRepository
    {
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
