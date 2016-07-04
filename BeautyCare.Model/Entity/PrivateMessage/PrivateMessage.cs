using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class PrivateMessage : BaseMessage<PrivateMessageAttachment>, IBeautyCareRepository
    {
        public virtual User Recipient { get; set; }
        public int RecipientId { get; set; }
    }
}
