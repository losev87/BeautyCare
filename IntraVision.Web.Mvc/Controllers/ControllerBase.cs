using System;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// Executes command, returns JsonResult, containing CommandResult with the information about the command. 
        /// If the command is executed successfully, CommandResult.Successful is set to true. Otherwise, errors are copied in CommandResult.Errors collection.
        /// </summary>
        /// <returns>JsonResult</returns>
        protected JsonResult ExecuteCommand(Action command)
        {
            if (!ViewData.ModelState.IsValid)
                return Json(new CommandResult(ViewData.ModelState));

            try
            {
                command.Invoke();
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("Global", ex);
            }
            return new JsonResult
                       {
                           Data = new CommandResult(ViewData.ModelState),
                           JsonRequestBehavior = JsonRequestBehavior.AllowGet
                       };
        }

        protected FileJsonResult ExecuteCommandWithFile(Action command)
        {
            if (!ViewData.ModelState.IsValid)
                return new FileJsonResult(new CommandResult(ViewData.ModelState))
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            try
            {
                command.Invoke();
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("Global", ex.GetBaseException());
            }
            return new FileJsonResult(new CommandResult(ViewData.ModelState))
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected JsonResult ExecuteQuery(Func<object> command)
        {
            if (!ViewData.ModelState.IsValid)
                return Json(new CommandResult(ViewData.ModelState));

            object data = null;
            try
            {
                data = command.Invoke();
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("Global", ex);
            }
            return Json(new CommandResult(ViewData.ModelState, data), JsonRequestBehavior.AllowGet);
        }

        protected ActionResult ExecuteCommand<TEnity, TResult>(TEnity o, Func<TResult> command)
        {
            if (!ViewData.ModelState.IsValid)
                return View(o);

            TResult data = default(TResult);
            try
            {
                data = command.Invoke();
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("Global", ex);
            }
            return View(data);
        }
    }
}
