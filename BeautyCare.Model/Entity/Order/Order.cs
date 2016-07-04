using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class Order : EntityBase, IBeautyCareRepository
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual User Executor { get; set; }
        public int ExecutorId { get; set; }

        public virtual User Customer { get; set; }
        public int CustomerId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public virtual OrderStatus Status { get; set; }
        public int StatusId { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual ICollection<OrderAttachment> Attachments { get; set; }
        public virtual ICollection<OrderUserService> Services { get; set; }
        public virtual ICollection<OrderMessage> Messages { get; set; }
    }
}
