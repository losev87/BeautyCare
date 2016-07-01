using System.Security.Principal;

namespace IntraVision.Web.Mvc.Security
{
    public interface IMVCIdentity : IIdentity
    {
        long Id { get; }
    }
}
