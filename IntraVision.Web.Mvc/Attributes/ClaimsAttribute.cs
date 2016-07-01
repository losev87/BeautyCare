using System;
using System.Collections.Generic;

namespace IntraVision.Web.Mvc
{
    public class ClaimsAttribute : Attribute
    {
        public KeyValuePair<string, string> Claim;

        public ClaimsAttribute(string claimType, string claimValue)
        {
            Claim = new KeyValuePair<string, string>(claimType, claimValue);
        }
    }
}
