using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntraVision.Data;

namespace BeautyCare.Model.Entity
{
    
    public class Publication : EntityBase, INamedEntityBase, IBeautyCareRepository
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Name { get; set; }

        //public int RegionId { get; set; }
        //public virtual Region Region { get; set; }

        //public virtual ICollection<Dealer> Dealers { get; set; }
    }
}
