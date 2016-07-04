namespace BeautyCare.Model.Entity
{
    public class OrderAttachment : BaseAttachment<OrderAttachmentData>, IBeautyCareRepository
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
