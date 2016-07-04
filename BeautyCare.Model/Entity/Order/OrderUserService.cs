using IntraVision.Web.Mvc.Entities;

namespace BeautyCare.Model.Entity
{
    public class OrderUserService : EntityBase, IBeautyCareRepository
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        public virtual UserService UserService { get; set; }
        public int UserServiceId { get; set; }

        public int Quantity { get; set; }
    }
}
