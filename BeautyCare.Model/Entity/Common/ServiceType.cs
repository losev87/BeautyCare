using System.Collections.Generic;
using BeautyCare.Model.Management;

namespace BeautyCare.Model.Entity
{
    public class ServiceType : BaseCatalog,IBeautyCareRepository
    {
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
