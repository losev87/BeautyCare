namespace IntraVision.Web.Mvc
{
    public class TokenInputAttribute : TargetPropertyAttribute, IAdditionalValueAttribute
    {
        public string Action { get; set; }
        public string Controller { get; set; }

        public TokenInputAttribute(string targetProperty) : base(targetProperty) { }
    }
}
