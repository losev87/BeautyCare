namespace BeautyCare.Model.Entity
{
    public class PublicationAttachment : BaseAttachment<PublicationAttachmentData>, IBeautyCareRepository
    {
        public virtual Publication Publication { get; set; }
        public int PublicationId { get; set; }
    }
}
