namespace IntraVision.Web.Mvc.Security
{
    public interface IUserPersister
    {
        void SetPrincipal(IMVCPrincipal user, bool rememberMe);
        IMVCPrincipal GetPrincipal();
        IMVCPrincipal GetPrincipalFromToken(string token);
    }
}
