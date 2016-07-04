using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{
    public class Answer : BaseRatedMessage<AnswerAttachment>,IBeautyCareRepository
    {
        public virtual Answer ParentAnswer { get; set; }
        public int? ParentAnswerId { get; set; }
        public virtual Question Question { get; set; }
        public int? QuestionId { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
