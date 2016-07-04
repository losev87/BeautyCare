using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class PrivateMessageAttachment : BaseAttachment<PrivateMessageAttachmentData>, IBeautyCareRepository
    {
        public virtual PrivateMessage PrivateMessage { get; set; }
        public int PrivateMessageId { get; set; }
    }
}
