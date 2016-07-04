using System.Collections.Generic;
using BeautyCare.Model.Management;

namespace BeautyCare.Model.Entity
{
    public class Gender : BaseCatalog, IBeautyCareRepository
    {
        public virtual ICollection<User> Users { get; set; }
    }
}
