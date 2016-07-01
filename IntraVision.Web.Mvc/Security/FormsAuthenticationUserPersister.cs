using System;
using System.Web;
using System.Web.Security;
using ServiceStack.Text;
using System.Text;

namespace IntraVision.Web.Mvc.Security
{
    public class FormsAuthenticationUserPersister<TIdentity> : IUserPersister
        where TIdentity : class, IMVCIdentity
    {
        public FormsAuthenticationUserPersister() { }

        #region IUserPersister Members

        public void SetPrincipal(IMVCPrincipal user, bool rememberMe)
        {
            var sb = new StringBuilder();
            //Store identity in JSON format
            sb.Append(JsonSerializer.SerializeToString(user.Identity as TIdentity));
            //Store roles for principal divided by "|"
            if (user.Roles != null && user.Roles.Length > 0)
                sb.Append("#").Append(string.Join("|", user.Roles));

            string userData = sb.ToString();

            //Create and encrypt authentication ticket
            var ticket = new FormsAuthenticationTicket(1, user.Identity.Name, DateTime.Now, DateTime.Now.AddDays(90), rememberMe, userData);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            //Store ticket in cookie
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //If Remember me was checked, set the expiration date to 90 days in advance
            if (rememberMe) faCookie.Expires = DateTime.Now.AddDays(90);

            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

        public IMVCPrincipal GetPrincipal()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (authCookie == null) return null;

            // Get the authentication ticket and rebuild the principal & identity
            try
            {
                return GetPrincipalFromToken(authCookie.Value);
            }
            catch
            {
                return null;
            }
        }

        public IMVCPrincipal GetPrincipalFromToken(string ticket)
        {
            var authTicket = FormsAuthentication.Decrypt(ticket);

            string userData = authTicket.UserData;
            int sharpIndex = userData.LastIndexOf('#');
            //Get serialized identity data
            string identityData = sharpIndex > 0 ? userData.Substring(0, sharpIndex) : userData;

            //Try reading roles from the end of userData. Roles are divided from the serialized identity with '#' and must be splitted by '|'.
            string[] roles = null;
            if (sharpIndex > 0 && sharpIndex < userData.Length - 1)
                roles = userData.Substring(sharpIndex + 1, userData.Length - sharpIndex - 1).Split('|');

            //Deserialize identity
            var identity = JsonSerializer.DeserializeFromString<TIdentity>(identityData);

            //Return principal with identity and roles
            return new MVCPrincipal(identity, roles);
        }
        #endregion
    }
}
