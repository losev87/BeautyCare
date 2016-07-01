using System;
using System.Web.Mvc;
using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Controls.Filter;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc
{
    public class FiltrateCRUDController<TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter> : CRUDController<TEntity, TEntityGrid, TEntityGridOptions>
        where TEntity : class, IEntityBase
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        private IFilterableBaseService<TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter> _filterableBaseService;
        public FiltrateCRUDController(IFilterableBaseService<TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter> filterableBaseService)
            : base((IBaseService<TEntity, TEntityGrid, TEntityGridOptions>)filterableBaseService)
        {
            _filterableBaseService = filterableBaseService;
        }

        [Claims("action", "read")]
        public override ActionResult Index()
        {
            return View("IndexWithFilter");
        }

        [Claims("action", "read")]
        public ActionResult Filter(TEntityGridOptions options)
        {
            string filterKey = GridOptionsModelBinder.GridKey(ControllerContext);
            return View(_filterableBaseService.GetFilter(options, filterKey, User));
        }

        public JsonResult SaveFilter(FilterEdit filterEdit, GridOptions options)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult LoadFilter(long id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult ClearFilter()
        {
            GridOptionsModelBinder.ClearGridOptions(ControllerContext);
            return RedirectToAction("Index");
        }
    }
}
