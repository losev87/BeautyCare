using System.Security.Principal;

namespace IntraVision.Web.Mvc.Security
{
    public interface IMVCPrincipal : IPrincipal
    {
        new IMVCIdentity Identity { get; }
        string[] Roles { get; }
    }
}
