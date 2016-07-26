using System;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Management;
using BeautyCare.Service;
using BeautyCare.ViewModel.AZ;
using IntraVision.Data;
using IntraVision.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BeautyCare.Controllers
{
    public class AccountController : AccountControllerBase<UserManager, User, Role, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>
    {
        public AccountController(Lazy<UserManager> service)
            : base(service)
        {

        }

        [HttpGet]
        public ActionResult ChangePassword(string login)
        {
            return View(new ChangePasswordModel { Login = login });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _service.Value.FindAsync(model.Login, model.OldPassword).Result;
                if (user != null)
                {
                    var resetPasswordResult = _service.Value.ResetPassword(user.Id,
                        _service.Value.GeneratePasswordResetToken(user.Id), model.NewPassword);

                    if (!resetPasswordResult.Succeeded)
                        ModelState.AddModelError("Global", resetPasswordResult.Errors.FirstOrDefault());
                }
                else
                    ModelState.AddModelError("Global", "Неправильный текущий пароль");
            }

            return View(model);

        }
    }
}
