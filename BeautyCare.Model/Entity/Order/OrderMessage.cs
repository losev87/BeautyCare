namespace BeautyCare.Model.Entity
{
    public class OrderMessage : BaseMessage<OrderMessageAttachment>, IBeautyCareRepository
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
