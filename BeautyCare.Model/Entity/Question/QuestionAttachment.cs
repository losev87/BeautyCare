namespace BeautyCare.Model.Entity
{
    public class QuestionAttachment : BaseAttachment<QuestionAttachmentData>, IBeautyCareRepository
    {
        public virtual Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
