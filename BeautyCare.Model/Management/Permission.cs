using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Entities;

namespace BeautyCare.Model.Management
{
    public class Permission : EntityBase, IBeautyCareRepository
    {
        [StringLength(100)]
        public string MvcController { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
