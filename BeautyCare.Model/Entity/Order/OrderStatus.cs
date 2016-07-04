using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class OrderStatus : EntityBase, IBeautyCareRepository
    {
        public string Name { get; set; }
        public string SysName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
