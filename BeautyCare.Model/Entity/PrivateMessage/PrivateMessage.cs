using BeautyCare.Model.Management;

namespace BeautyCare.Model.Entity
{
    public class PrivateMessage : BaseMessage<PrivateMessageAttachment>, IBeautyCareRepository
    {
        public virtual User Recipient { get; set; }
        public int RecipientId { get; set; }
    }
}
