using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{
    public class ColorCategory : BaseCatalog, IBeautyCareRepository
    {
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
