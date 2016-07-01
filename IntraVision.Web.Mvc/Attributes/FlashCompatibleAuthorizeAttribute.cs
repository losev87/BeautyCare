﻿using System;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Security;

namespace IntraVision.Web.Mvc.Attributes
{
    /// <summary>
    /// A custom version of the <see cref="AuthorizeAttribute"/> that supports working
    /// around a cookie/session bug in Flash.  
    /// </summary>
    /// <remarks>
    /// Details of the bug and workaround can be found on this blog:
    /// http://geekswithblogs.net/apopovsky/archive/2009/05/06/working-around-flash-cookie-bug-in-asp.net-mvc.aspx
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class FlashCompatibleAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// The key to the authentication token that should be submitted somewhere in the request.
        /// </summary>
        private const string TOKEN_KEY = "AuthenticationToken";

        /// <summary>
        /// This changes the behavior of AuthorizeCore so that it will only authorize
        /// users if a valid token is submitted with the request.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            string token = httpContext.Request.Params[TOKEN_KEY];

            if (token != null)
            {
                var userPersister = GetInLifetimeScope.Instance<IUserPersister>();
                var principal = userPersister.GetPrincipalFromToken(token);

                httpContext.User = principal;
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}
