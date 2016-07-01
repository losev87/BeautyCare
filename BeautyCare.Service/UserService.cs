using System;
using System.Linq;
using System.Security.Principal;
using BeautyCare.Model.Management;
using BeautyCare.ViewModel.AZ.User;
using IntraVision.Web.Mvc.Controls;
using Microsoft.AspNet.Identity;

namespace BeautyCare.Service
{
    public class UserService : IUserService, IServiceImplementation
    {
        private Lazy<UserManager> _userManager;

        public UserService(
            Lazy<UserManager> userManager)
        {
            _userManager = userManager;
        }

        public ActionGrid<UserView, UserGrid> Grid(UserGridOptions options, IPrincipal principal)
        {
            var query = _userManager.Value.Users
                .Select(u => new UserView
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                });

            return new ActionGrid<UserView, UserGrid>(query, options);
        }

        public void Create(UserCreate create, IPrincipal principal)
        {
            var user = new User
            {
                UserName = create.Login,
                Email = create.Email
            };
            var userCreateResult = _userManager.Value.Create(user, create.Password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(userCreateResult.Errors.FirstOrDefault());
            }
        }

        public UserEdit Edit(int id, IPrincipal principal)
        {
            var user = _userManager.Value.FindById(id);
            if (user != null)
            {
                var editUser = new UserEdit
                {
                    Id = user.Id,
                    Login = user.UserName,
                    Email = user.Email
                };
                return editUser;
            }
            return null;
        }

        public void Edit(UserEdit edit, IPrincipal principal)
        {
            var user = _userManager.Value.FindById(edit.Id);
            if (user != null)
            {
                user.UserName = edit.Login;
                user.Email = edit.Email;

                var updateResult = _userManager.Value.Update(user);

                if (!updateResult.Succeeded)
                {
                    throw new Exception(updateResult.Errors.FirstOrDefault());
                }

                if (!string.IsNullOrWhiteSpace(edit.Password))
                {
                    var resetPasswordResult = _userManager.Value.ResetPassword(edit.Id,
                        _userManager.Value.GeneratePasswordResetToken(edit.Id), edit.Password);

                    if (!resetPasswordResult.Succeeded)
                        throw new Exception(resetPasswordResult.Errors.FirstOrDefault());
                }
            }
        }

        public void Delete(int id, IPrincipal principal)
        {
            var user = _userManager.Value.FindById(id);
            if (user != null)
            {
                var deleteResult = _userManager.Value.Delete(user);

                if (!deleteResult.Succeeded)
                {
                    throw new Exception(deleteResult.Errors.FirstOrDefault());
                }
            }
        }
    }
}
