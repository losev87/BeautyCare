using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc
{
    public class CrudController<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> : ControllerBase
        where TEntity : class, IEntityBase
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        public string PrefixView = "";

        public string FileNameXlsxGrid = string.Format("XlsxGrid_{0}", DateTime.Now.ToString("dd-MM-yyyy_HH-mm"));

        public IBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> _service { get; set; }

        public CrudController(IBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> service)
        {
            _service = service;
        }

        public virtual ActionResult Index()
        {
            return View(PrefixView + "Index");
        }

        public virtual ActionResult Grid(TEntityGridOptions options)
        {
            return View("Grid", _service.GetActionGridTEntityList(options, User));
        }

        public virtual ActionResult XlsxGrid(TEntityGridOptions options)
        {
            byte[] reportData = _service.GetActionGridTEntityList(options, User).XlsxRender().ToArray();

            Response.ContentType = "application/vnd.openxmlformat";
            Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.xlsx", FileNameXlsxGrid));
            return File(reportData, "application/vnd.openxmlformat");
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(PrefixView + "Create", _service.Create(User));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntityCreate create)
        {
            if (!ViewData.ModelState.IsValid)
                return View(create);

            return ExecuteCommandWithFile(() => _service.Create(create, User));
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            return View(PrefixView + "Edit", _service.Edit(id, User));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEntityEdit edit)
        {
            if (!ViewData.ModelState.IsValid)
                return View(edit);

            return ExecuteCommandWithFile(() => _service.Edit(edit, User));
        }

        public virtual ActionResult Display(int id)
        {
            return View(PrefixView + "Display", _service.Edit(id, User));
        }

        [HttpDelete]
        public virtual JsonResult Delete(int id)
        {
            return ExecuteCommand(() => _service.Delete(id, User));
        }

        [HttpPost]
        public virtual JsonResult Sortable(Dictionary<int, int> rows)
        {
            return ExecuteCommand(() => _service.Sortable(rows));
        }

        [HttpGet]
        public virtual ActionResult Image(string image, int id)
        {
            return _service.Image(image, id);
        }
    }
}
