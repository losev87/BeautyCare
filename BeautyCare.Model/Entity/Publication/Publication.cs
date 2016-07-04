using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{

    public class Publication : BaseRatedMessage<PublicationAttachment>, IBeautyCareRepository
    {
        public string Header { get; set; }
        public virtual ICollection<ColorCategory> Colors { get; set; }
        public virtual ICollection<ServiceType> ServiceTypes { get; set; } 
        public virtual ICollection<HashTag> HashTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
