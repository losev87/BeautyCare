
namespace IntraVision.Web.Mvc.Controls.Filter
{
    public class SavedFilter
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string FilterKey { get; set; }
        public virtual long? UserId { get; set; }
        public virtual string GridOptions { get; set; }
        public virtual bool IsDefault { get; set; }
    }
}