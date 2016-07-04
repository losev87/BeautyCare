using System.Collections.Generic;

namespace BeautyCare.Model.Entity
{
    public class OrderStatus : BaseCatalog, IBeautyCareRepository
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}
