using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class OrderUserService : EntityBase, IBeautyCareRepository
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        public virtual UserService UserService { get; set; }
        public int UserServiceId { get; set; }

        public int Quantity { get; set; }
    }
}
