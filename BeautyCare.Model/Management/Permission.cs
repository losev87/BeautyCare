using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IntraVision.Data;

namespace BeautyCare.Model.Management
{
    public class Permission : EntityBase
    {
        [StringLength(100)]
        public string MvcController { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
