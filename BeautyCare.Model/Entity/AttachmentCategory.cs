using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{

    public class AttachmentCategory : EntityBase
    {
        public string Name { get; set; }
        public string SysName { get; set; }

        public virtual ICollection<OrderAttachment> OrderAttachments { get; set; }
        public virtual ICollection<PrivateMessageAttachment> PrivateMessageAttachments { get; set; }
        public virtual ICollection<OrderMessageAttachment> OrderMessageAttachments { get; set; }
    }
}
