using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace IntraVision.Web.Mvc.Services
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override Task<IdentityResult> ValidateAsync(string item)
        {
            var chars = item.ToCharArray();

            if (String.IsNullOrEmpty(item) || item.Length < RequiredLength)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("Длина пароля не должна быть менее {0}", RequiredLength)));
            }

            if (RequireNonLetterOrDigit && chars.Where(IsLetterOrDigit).Count() < 2)
            {
                return Task.FromResult(IdentityResult.Failed("Пароль должен содержать минимум 2 специальных символа"));
            }

            if (RequireDigit && chars.Where(IsDigit).Count() < 2)
            {
                return Task.FromResult(IdentityResult.Failed("Пароль должен содержать минимум 2 цифры"));
            }

            if (RequireLowercase && chars.Where(IsLower).Count() < 2)
            {
                return Task.FromResult(IdentityResult.Failed("Пароль должен содержать минимум 2 символа нижнего регистра"));
            }

            if (RequireUppercase && chars.Where(IsUpper).Count() < 2)
            {
                return Task.FromResult(IdentityResult.Failed("Пароль должен содержать минимум 2 символа верхнего регистра"));
            }

            return base.ValidateAsync(item);
        }
    }
}
