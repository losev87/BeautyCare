using System;
using System.Collections.Generic;
using BeautyCare.Model.Management;
using IntraVision.Web.Mvc.Entities;

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
