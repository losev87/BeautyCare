namespace BeautyCare.Model.Entity
{
    public class PrivateMessageAttachment : BaseAttachment<PrivateMessageAttachmentData>, IBeautyCareRepository
    {
        public virtual PrivateMessage PrivateMessage { get; set; }
        public int PrivateMessageId { get; set; }
    }
}
