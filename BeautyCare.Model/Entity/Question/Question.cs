using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{

    public class Question : BaseRatedMessage<QuestionAttachment>, IBeautyCareRepository
    {
        public string Header { get; set; }
        public virtual ICollection<ColorCategory> Colors { get; set; }
        public virtual ICollection<ServiceType> ServiceTypes { get; set; } 
        public virtual ICollection<HashTag> HashTags { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
