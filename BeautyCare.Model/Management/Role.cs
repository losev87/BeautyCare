using System.Collections.Generic;
using BeautyCare.Model.Entity;
using IntraVision.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyCare.Model.Management
{
    public class Role : IdentityRole<int, UserRole>, IEntityBase, IBeautyCareRepository
    {
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
