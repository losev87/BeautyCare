using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{
    public class Comment : BaseRatedMessage<CommentAttachment>,IBeautyCareRepository
    {
        public virtual Comment ParentComment { get; set; }
        public int? ParentCommentId { get; set; }
        public virtual Publication Publication { get; set; }
        public int? PublicationId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
