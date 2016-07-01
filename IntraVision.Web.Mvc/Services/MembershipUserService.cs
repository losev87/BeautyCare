using System;
using System.Web.Security;

namespace IntraVision.Web.Mvc.Services
{
    public class MembershipUserService : IMembershipUserService
    {
        private readonly MembershipProvider _provider;

        public MembershipUserService()
            : this(null)
        {
        }

        public MembershipUserService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipUser CreateUser(string userName, string password, string email, out MembershipCreateStatus membershipCreateStatus)
        {
            return _provider.CreateUser(userName, password, email, null, null, true, null, out membershipCreateStatus);
        }

        public string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists.";//Please enter a different user name.

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }


        public bool DeleteUser(string userName)
        {
            return _provider.DeleteUser(userName, true);
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return _provider.ChangePassword(userName, oldPassword, newPassword);
        }

        public string ResetPassword(string userName)
        {
            return _provider.ResetPassword(userName, null);
        }

        public MembershipUser GetUser(string userName)
        {
            return _provider.GetUser(userName, false);
        }
    }
}
