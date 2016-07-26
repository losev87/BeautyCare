using System;
using System.Web.Mvc;
using BeautyCare.Service;
using BeautyCare.ViewModel.AZ;


namespace BeautyCare.Controllers.AZ.Controllers
{
    [Authorize]
    public class UserController : IntraVision.Web.Mvc.ControllerBase
    {
        private Lazy<IUserService> _service;

        public UserController(Lazy<IUserService> service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            ViewBag.EntityName = "Users";
            return View();
        }

        public virtual ActionResult Grid(UserGridOptions options)
        {
            return View("Grid", _service.Value.Grid(options, User));
        }

        public virtual ActionResult XlsxGrid(UserGridOptions options)
        {
            byte[] reportData = _service.Value.Grid(options, User).XlsxRender().ToArray();

            Response.ContentType = "application/vnd.openxmlformat";
            Response.AppendHeader("Content-Disposition", "attachment;filename=users.xlsx");
            return File(reportData, "application/vnd.openxmlformat");
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(new UserCreate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(UserCreate create)
        {
            if (!ViewData.ModelState.IsValid)
                return View(create);

            return ExecuteCommandWithFile(() => _service.Value.Create(create, User));
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            return View(_service.Value.Edit(id, User));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(UserEdit edit)
        {
            if (!ViewData.ModelState.IsValid)
                return View(edit);

            try
            {
                _service.Value.Edit(edit, User);
            }
            catch (Exception e)
            {
                ViewData.ModelState.AddModelError("", e.Message);
            }
            return View(edit);
        }

        [HttpDelete]
        public virtual JsonResult Delete(int id)
        {
            return ExecuteCommand(() => _service.Value.Delete(id, User));
        }
    }
}
