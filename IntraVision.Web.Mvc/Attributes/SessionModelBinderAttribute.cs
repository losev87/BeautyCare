using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public class SessionModelBinderAttribute : CustomModelBinderAttribute
    {
        private readonly string _sessionKey;

        public SessionModelBinderAttribute(string sessionKey)
        {
            _sessionKey = sessionKey;
        }

        public override IModelBinder GetBinder()
        {
            return new SessionModelBinder(_sessionKey);
        }
    }
}
