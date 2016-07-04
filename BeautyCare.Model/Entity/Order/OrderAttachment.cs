using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class OrderAttachment : BaseAttachment<OrderAttachmentData>, IBeautyCareRepository
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
