using System.Web.Security;

namespace IntraVision.Web.Mvc.Services
{
    public interface IMembershipUserService
    {
        bool ValidateUser(string userName, string password);
        MembershipUser CreateUser(string userName, string password, string email, out MembershipCreateStatus membershipCreateStatus);
        bool DeleteUser(string userName);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        string ErrorCodeToString(MembershipCreateStatus createStatus);
        string ResetPassword(string userName);
        MembershipUser GetUser(string userName);
    }
}
