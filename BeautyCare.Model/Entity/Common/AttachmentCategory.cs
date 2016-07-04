using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{
    public class AttachmentCategory : BaseCatalog, IBeautyCareRepository
    {
        public virtual ICollection<OrderAttachment> OrderAttachments { get; set; }
        public virtual ICollection<PrivateMessageAttachment> PrivateMessageAttachments { get; set; }
        public virtual ICollection<OrderMessageAttachment> OrderMessageAttachments { get; set; }
        public virtual ICollection<PublicationAttachment> PublicationAttachments { get; set; }
        public virtual ICollection<CommentAttachment> CommentAttachments { get; set; }
        public virtual ICollection<QuestionAttachment> QuestionAttachments { get; set; }
        public virtual ICollection<AnswerAttachment> AnswerAttachments { get; set; }
    }
}
