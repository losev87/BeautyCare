using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{
    public class HashTag : BaseCatalog,IBeautyCareRepository
    {
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
