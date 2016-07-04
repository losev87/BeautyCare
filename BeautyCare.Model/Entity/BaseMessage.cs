using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class BaseMessage<TAttachment> : EntityBase
        where TAttachment : EntityBase
    {
        public virtual User Sender { get; set; }
        public int SenderId { get; set; }

        public string Text { get; set; }

        public DateTime SendDateTime { get; set; }

        public virtual ICollection<TAttachment> Attachments { get; set; }
    }
}
