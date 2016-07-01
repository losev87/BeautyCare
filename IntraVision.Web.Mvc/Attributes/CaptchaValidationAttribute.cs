using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IntraVision.Web.Mvc
{
    public class CaptchaValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            var key = string.Format("CaptchaValidationText_{0}", HttpContext.Current.Request.QueryString["captchaKey"]);
            var validationText = HttpContext.Current.Session[key];
            HttpContext.Current.Session[key] = new Random().Next();
            return value.ToString() == validationText.ToString();
        }
    }
}
