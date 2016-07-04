namespace BeautyCare.Model.Entity
{
    public class AnswerAttachment : BaseAttachment<AnswerAttachmentData>, IBeautyCareRepository
    {
        public virtual Answer Answer { get; set; }
        public int AnswerId { get; set; }
    }
}
