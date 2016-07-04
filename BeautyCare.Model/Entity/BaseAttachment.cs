using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    public class BaseAttachment<TAttachmentData> : EntityBase
        where TAttachmentData: EntityBaseFile
    {
        public int? AttachmentCategoryId { get; set; }

        public virtual AttachmentCategory AttachmentCategory { get; set; }

        public virtual TAttachmentData Data { get; set; }

        public string Extension { get; set; }
    }
}
