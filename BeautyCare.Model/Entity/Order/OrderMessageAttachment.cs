namespace BeautyCare.Model.Entity
{
    public class OrderMessageAttachment : BaseAttachment<OrderMessageAttachmentData>, IBeautyCareRepository
    {
        public virtual OrderMessage OrderMessage { get; set; }
        public int OrderMessageId { get; set; }
    }
}
