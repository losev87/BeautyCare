using BeautyCare.Model.Management;
using IntraVision.Web.Mvc.Entities;

namespace BeautyCare.Model.Entity
{
    
    public class UserService : EntityBase, IBeautyCareRepository
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public decimal Price { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
