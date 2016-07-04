using BeautyCare.Model.Management;
using IntraVision.Web.Mvc.Entities;

namespace BeautyCare.Model.Entity
{
    public class UserPhoto : EntityBase, IBeautyCareRepository
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual UserPhotoData Data { get; set; }

        public string Extension { get; set; }
    }
}
