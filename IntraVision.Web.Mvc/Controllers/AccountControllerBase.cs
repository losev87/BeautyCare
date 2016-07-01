using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IntraVision.Data;
using IntraVision.Web.Mvc.Security;
using IntraVision.Web.Mvc.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IntraVision.Web.Mvc
{
    [Authorize]
    public class AccountControllerBase<TService, TUser, TRole, TUserLogin, TUserRole, TUserClaim> : ControllerBase
        where TService : UserManagerBase<TUser, TRole, TUserLogin, TUserRole, TUserClaim>
        where TUser : IdentityUser<int, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<int, TUserRole>
        where TUserLogin : IdentityUserLoginBase, new()
        where TUserRole : IdentityUserRoleGenericBase<TUser, TRole>, new()
        where TUserClaim : IdentityUserClaimBase, new()
    {
        protected Lazy<TService> _service;

        public AccountControllerBase(Lazy<TService> service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LogOnViewData model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _service.Value.FindAsync(model.Login, model.Password);
                if (user != null)
                {
                    await _service.Value.SignInAsync(user, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Некорректный имя пользователя или пароль.");
                }
            }

            return View(model);
        }

        public ActionResult RenderUserInfo()
        {
            return View(new UserInfo { Login = User.Identity.Name, Email = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _service.Value.SignOut(new[] { DefaultAuthenticationTypes.ApplicationCookie });
            return RedirectToRoute(((System.Web.Routing.Route)(RouteData.Route)).Defaults);
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToRoute(((System.Web.Routing.Route)(RouteData.Route)).Defaults);
            }
        }
    }
}
