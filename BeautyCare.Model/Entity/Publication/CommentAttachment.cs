namespace BeautyCare.Model.Entity
{
    public class CommentAttachment : BaseAttachment<CommentAttachmentData>, IBeautyCareRepository
    {
        public virtual Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}
