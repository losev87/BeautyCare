using System.Web.Mvc;
using IntraVision.Data;

namespace IntraVision.Web.Mvc.Entities
{
    public class EntityBase : IEntityBase
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}
