using IntraVision.Data;
using EntityBase = IntraVision.Web.Mvc.Entities.EntityBase;

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
