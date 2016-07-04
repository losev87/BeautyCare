using IntraVision.Web.Mvc.Entities;

namespace BeautyCare.Model.Entity
{
    public class BaseRatedMessage<TAttachment> : BaseMessage<TAttachment> 
        where TAttachment : EntityBase
    {
        public int Rating { get; set; }
    }
}
