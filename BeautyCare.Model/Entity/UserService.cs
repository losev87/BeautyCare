using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeautyCare.Model.Management;
using IntraVision.Data;
using IntraVision.Web.Mvc.Autofac;

namespace BeautyCare.Model.Entity
{
    
    public class UserService : EntityBase
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public decimal Price { get; set; }

        public virtual Service Service { get; set; }
        public int ServiceId { get; set; }

        //public int RegionId { get; set; }
        //public virtual Region Region { get; set; }

        //public virtual ICollection<Dealer> Dealers { get; set; }
    }
}
